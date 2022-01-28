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

    private PlaySession CurrentSession => DataContext as PlaySession;

    public MainWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _serviceProvider = serviceProvider;

        DataContext = new PlaySession();

        _canPlay = true;
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

    private void StartGame(Enums.PlayerType lightPlayer, Enums.PlayerType darkPlayer)
    {
        CurrentSession.CurrentGame.MoveHistory.CollectionChanged -= MoveHistoryChangedHandler;
        CurrentSession.CurrentGame.GameEnded += GameEndedHandler;

        CurrentSession.StartGame(lightPlayer, darkPlayer);

        CurrentSession.CurrentGame.MoveHistory.CollectionChanged += MoveHistoryChangedHandler;
        CurrentSession.CurrentGame.GameEnded += GameEndedHandler;

        _canPlay = true;
    }

    private void GameEndedHandler(object sender, Models.CustomEventArgs.GameEndedEventArgs e)
    {
        _canPlay = false;
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

        CurrentSession.CurrentGame.SelectSquare(square);
    }

    private void MoveHistoryChangedHandler(object sender,
        System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        MoveHistoryDataGrid.ScrollIntoView(CurrentSession.CurrentGame.MoveHistory.Last());
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
                Filter = "JSON data (*.json)|*.json"
            };

        if (dialog.ShowDialog() == true)
        {
            File.WriteAllText(dialog.FileName, CurrentSession.GetSerializedMoveHistory());
        }
    }
}