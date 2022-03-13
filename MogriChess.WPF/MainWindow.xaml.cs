using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using MogriChess.Models;
using MogriChess.ViewModels;
using MogriChess.WPF.Windows;

namespace MogriChess.WPF;

public partial class MainWindow : Window
{
    private readonly IServiceProvider _serviceProvider;
    private bool _canPlay;

    private Game CurrentSession => DataContext as Game;

    public MainWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _serviceProvider = serviceProvider;

        var gameConfig = _serviceProvider.GetRequiredService<GameConfig>();

        DataContext = new Game(gameConfig);

        _canPlay = true;

#if DEBUG
        BotVsBotMenuOption.Visibility = Visibility.Visible;
#endif
    }

    private void LightHumanDarkBot_OnClick(object sender, RoutedEventArgs e)
    {
        StartGame(Enums.PlayerType.Human, Enums.PlayerType.Bot);
    }

    private void LightBotDarkHuman_OnClick(object sender, RoutedEventArgs e)
    {
        StartGame(Enums.PlayerType.Bot, Enums.PlayerType.Human);
    }

    private void LightHumanDarkHuman_OnClick(object sender, RoutedEventArgs e)
    {
        StartGame(Enums.PlayerType.Human, Enums.PlayerType.Human);
    }

    private void LightBotDarkBot_OnClick(object sender, RoutedEventArgs e)
    {
        StartGame(Enums.PlayerType.Bot, Enums.PlayerType.Bot);
    }

    private void StartGame(Enums.PlayerType lightPlayer, Enums.PlayerType darkPlayer)
    {
        CurrentSession.MoveHistory.CollectionChanged -= MoveHistoryChangedHandler;
        CurrentSession.GameEnded -= GameEndedHandler;

        CurrentSession.StartGame(lightPlayer, darkPlayer);

        CurrentSession.MoveHistory.CollectionChanged += MoveHistoryChangedHandler;
        CurrentSession.GameEnded += GameEndedHandler;

        _canPlay = true;
    }

    private void GameEndedHandler(object sender, Models.CustomEventArgs.GameEndedEventArgs e)
    {
        switch (e.GameEndStatus)
        {
            case Enums.GameStatus.CheckmateByLight:
                DisplayGameEndMessage("Light player won");
                break;
            case Enums.GameStatus.CheckmateByDark:
                DisplayGameEndMessage("Dark player won");
                break;
            case Enums.GameStatus.Stalemate:
                DisplayGameEndMessage("Stalemate - tie");
                break;
            case Enums.GameStatus.DrawNoCaptures:
                DisplayGameEndMessage($"Draw ({Game.MAX_MOVES_WITHOUT_CAPTURE} moves without event) - tie");
                break;
            default:
                DisplayGameEndMessage("Unexpected game ending");
                break;
        }

        _canPlay = false;
    }

    private void DisplayGameEndMessage(string message)
    {
        var messageBox = new OK("Game over", message);
        messageBox.Owner = this;

        messageBox.ShowDialog();
    }

    private void ClickedOnSquare_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (!_canPlay)
        {
            return;
        }

        if (sender is not Canvas selectedSquare)
        {
            return;
        }

        Square square = selectedSquare.DataContext as Square;

        CurrentSession.SelectSquare(square);
    }

    private void MoveHistoryChangedHandler(object sender,
        System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        MoveHistoryDataGrid.UpdateLayout();
        MoveHistoryDataGrid.ScrollIntoView(CurrentSession.MoveHistory.Last());
    }

    private void Exit_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void Help_OnClick(object sender, RoutedEventArgs e)
    {
        Help help = _serviceProvider.GetRequiredService<Help>();
        help.Owner = this;
        help.Show();
    }

    private void About_OnClick(object sender, RoutedEventArgs e)
    {
        About about = _serviceProvider.GetRequiredService<About>();
        about.Owner = this;
        about.ShowDialog();
    }

    private void WriteCurrentGameState_OnClick(object sender, RoutedEventArgs e)
    {
        SaveFileDialog dialog =
            new SaveFileDialog
            {
                Title = "Save Current Game",
                Filter = "JSON data (*.json)|*.json"
            };

        if (dialog.ShowDialog() == true)
        {
            File.WriteAllText(dialog.FileName, CurrentSession.GetSerializedGameState());
        }
    }

    private void WriteMoveHistory_OnClick(object sender, RoutedEventArgs e)
    {
        SaveFileDialog dialog =
            new SaveFileDialog
            {
                Title = "Save Move History",
                Filter = "JSON data (*.json)|*.json"
            };

        if (dialog.ShowDialog() == true)
        {
            File.WriteAllText(dialog.FileName, CurrentSession.GetSerializedMoveHistory());
        }
    }

    private void DisplayRankAndFileLabels_OnChecked(object sender, RoutedEventArgs e)
    {
        CurrentSession.DisplayRankFileLabels = true;
    }

    private void DisplayRankAndFileLabels_OnUnchecked(object sender, RoutedEventArgs e)
    {
        CurrentSession.DisplayRankFileLabels = false;
    }

    private void DisplayValidMoves_OnChecked(object sender, RoutedEventArgs e)
    {
        CurrentSession.DisplayValidDestinations = true;
    }

    private void DisplayValidMoves_OnUnchecked(object sender, RoutedEventArgs e)
    {
        CurrentSession.DisplayValidDestinations = false;
    }
}