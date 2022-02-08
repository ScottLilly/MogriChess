using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MogriChess.Core;
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
        // If square doesn't have a piece, return
        // If piece is not current player's color, return
        if (CurrentGame.SelectedSquare == null &&
            (square.Piece == null ||
             square.Piece.Color != CurrentGame.CurrentPlayerColor))
        {
            return;
        }

        // If SelectedSquare == null, select the square
        if (CurrentGame.SelectedSquare == null)
        {
            CurrentGame.SelectedSquare = square;
            PopulateValidDestinations();

            return;
        }

        // If passed-in square is the SelectedSquare, unselect it
        if (CurrentGame.SelectedSquare == square)
        {
            CurrentGame.SelectedSquare = null;
            ClearValidDestinations();

            return;
        }

        // If SelectedSquare != null:
        // If DestinationSquare is in ValidDestinations, perform move
        // otherwise, do nothing
        if (CurrentGame.ValidDestinationsForSelectedPiece.Any(m =>
                m.DestinationSquare.SquareShorthand == square.SquareShorthand))
        {
            CurrentGame.MoveToSelectedSquare(square);
        }
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

    private void PopulateValidDestinations()
    {
        ClearValidDestinations();

        foreach (Move move in CurrentGame.LegalMovesForSelectedPiece())
        {
            CurrentGame.ValidDestinationsForSelectedPiece.Add(move);
            move.DestinationSquare.IsValidDestination = DisplayValidDestinations;
        }
    }

    private void ClearValidDestinations()
    {
        CurrentGame.ValidDestinationsForSelectedPiece.Clear();
        CurrentGame.Board.ClearValidDestinations();
    }

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