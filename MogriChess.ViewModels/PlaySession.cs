using System;
using System.ComponentModel;
using MogriChess.Models;
using MogriChess.Services;

namespace MogriChess.ViewModels;

public class PlaySession : INotifyPropertyChanged
{
    private bool _displayRankFileLabels;
    private bool _displayValidDestinations;
    public event PropertyChangedEventHandler PropertyChanged;

    public Game CurrentGame { get; }

    public bool DisplayRankFileLabels
    {
        get => _displayRankFileLabels;
        set
        {
            _displayRankFileLabels = value;
            CurrentGame.DisplayRankFileLabel = DisplayRankFileLabels;
        }
    }

    public bool DisplayValidDestinations
    {
        get => _displayValidDestinations;
        set
        {
            _displayValidDestinations = value;
            CurrentGame.DisplayValidDestinations = DisplayValidDestinations;
        }
    }

    public PlaySession()
    {
        CurrentGame = GameFactory.GetNewGame();

        CurrentGame.MoveCompleted += MoveCompletedHandler;
    }

    public void StartGame(Enums.PlayerType lightPlayer = Enums.PlayerType.Human,
        Enums.PlayerType darkPlayer = Enums.PlayerType.Human)
    {
        CurrentGame.LightPlayerBot =
            lightPlayer == Enums.PlayerType.Bot
                ? new BotPlayer(Enums.Color.Light,
                    new PieceValueCalculator(
                        new PieceValueCalculatorGenome(1,2,5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 999)))
                : null;

        CurrentGame.DarkPlayerBot =
            darkPlayer == Enums.PlayerType.Bot
                ? new BotPlayer(Enums.Color.Dark,
                    new PieceValueCalculator(
                        new PieceValueCalculatorGenome(1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 999)))
                : null;

        BoardFactory.PopulateBoardWithStartingPieces(CurrentGame.Board);
        CurrentGame.StartGame();

        if (CurrentGame.LightPlayerBot != null)
        {
            MakeBotMove();
        }
    }

    public void SelectSquare(Square square)
    {
        CurrentGame.SelectSquare(square);
    }

    public string GetSerializedGameState()
    {
        return BoardStateService.GetSerializedGameState(CurrentGame);
    }

    public string GetSerializedMoveHistory()
    {
        return BoardStateService.GetSerializedMoveHistory(CurrentGame);
    }

    #region Private methods

    private void MoveCompletedHandler(object sender, EventArgs e)
    {
        if (CurrentGame.Status != Enums.GameStatus.Playing)
        {
            // Game has ended
            return;
        }

        MakeBotMove();
    }

    private void MakeBotMove()
    {
        if (CurrentGame.CurrentPlayerColor == Enums.Color.Dark &&
            CurrentGame.DarkPlayerBot != null)
        {
            CurrentGame.MakeBotMove(CurrentGame.DarkPlayerBot);
        }

        if (CurrentGame.CurrentPlayerColor == Enums.Color.Light &&
            CurrentGame.LightPlayerBot != null)
        {
            CurrentGame.MakeBotMove(CurrentGame.LightPlayerBot);
        }
    }

    #endregion
}