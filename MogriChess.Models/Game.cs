using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using MogriChess.Core;
using MogriChess.Models.CustomEventArgs;

namespace MogriChess.Models;

public class Game : INotifyPropertyChanged
{
    private Enums.Color _currentPlayerColor = Enums.Color.NotSelected;
    private IEnumerable<Move> _legalMovesForCurrentPlayer;
    private Enums.GameStatus _status = Enums.GameStatus.Preparing;
    private Square _selectedSquare;

    public Enums.Color CurrentPlayerColor
    {
        get => _currentPlayerColor;
        private set
        {
            _currentPlayerColor = value;

            if (_currentPlayerColor == Enums.Color.NotSelected)
            {
                return;
            }

            CacheLegalMovesForCurrentPlayer();

            if (_legalMovesForCurrentPlayer.None())
            {
                Status = Enums.GameStatus.Stalemate;
            }
        }
    }

    public Enums.GameStatus Status
    {
        get => _status;
        private set
        {
            // If status changed to game-ending status, handle events and end game
            if (_status != value &&
                _status == Enums.GameStatus.Playing)
            {
                if (value == Enums.GameStatus.Stalemate)
                {
                    GameEnded?.Invoke(this, new GameEndedEventArgs(Enums.GameStatus.Stalemate));
                }
                else if (value == Enums.GameStatus.CheckmateByLight)
                {
                    GameEnded?.Invoke(this, new GameEndedEventArgs(Enums.GameStatus.CheckmateByLight));
                }
                else if (value == Enums.GameStatus.CheckmateByDark)
                {
                    GameEnded?.Invoke(this, new GameEndedEventArgs(Enums.GameStatus.CheckmateByDark));
                }
            }

            _status = value;
        }
    }

    public Board Board { get; }
    public ObservableCollection<Move> MoveHistory { get; } =
        new ObservableCollection<Move>();
    public bool DisplayRankFileLabel { get; set; } = true;
    public bool DisplayValidDestinations { get; set; } = true;
    public BotPlayer LightPlayerBot { get; set; }
    public BotPlayer DarkPlayerBot { get; set; }

    public Square SelectedSquare
    {
        get => _selectedSquare;
        set
        {
            if (SelectedSquare != null)
            {
                SelectedSquare.IsSelected = false;
            }

            _selectedSquare = value;

            if (SelectedSquare != null)
            {
                SelectedSquare.IsSelected = true;
            }
        }
    }

    public ObservableCollection<Move> ValidDestinationsForSelectedPiece { get; } =
        new ObservableCollection<Move>();

    public event EventHandler MoveCompleted;
    public event EventHandler<GameEndedEventArgs> GameEnded;
    public event PropertyChangedEventHandler PropertyChanged;

    public Game(Board board)
    {
        Board = board;
    }

    #region Public methods

    public void StartGame()
    {
        // Clear out game
        CurrentPlayerColor = Enums.Color.NotSelected;
        SelectedSquare = null;
        Board.ClearValidDestinations();
        MoveHistory.Clear();

        // Start game
        CurrentPlayerColor = Enums.Color.Light;
        Status = Enums.GameStatus.Playing;
    }

    #endregion

    #region Private methods

    private void CacheLegalMovesForCurrentPlayer() =>
        _legalMovesForCurrentPlayer =
            Board.LegalMovesForPlayer(_currentPlayerColor);

    public List<Move> LegalMovesForSelectedPiece()
    {
        if (_legalMovesForCurrentPlayer == null)
        {
            CacheLegalMovesForCurrentPlayer();
        }

        if (SelectedSquare == null)
        {
            return new List<Move>();

        }

        return _legalMovesForCurrentPlayer
            .Where(m => m.OriginationSquare.SquareShorthand == SelectedSquare.SquareShorthand)
            .ToList();
    }

    public void MoveToSelectedSquare(Square square)
    {
        // Check that the destination square is a valid move
        Move move =
            ValidDestinationsForSelectedPiece.FirstOrDefault(d =>
                d.DestinationSquare.SquareShorthand == square.SquareShorthand);

        if (move == null)
        {
            return;
        }

        Board.MovePiece(SelectedSquare, square);

        DetermineIfMovePutsOpponentInCheckOrCheckmate(move);

        MoveHistory.Add(move);

        EndCurrentPlayerTurn();
    }

    private void DetermineIfMovePutsOpponentInCheckOrCheckmate(Move move)
    {
        if (Board.KingCanBeCaptured(move.MovingPieceColor.OppositeColor()))
        {
            move.PutsOpponentInCheck = true;
            move.PutsOpponentInCheckmate =
                Board.PlayerIsInCheckmate(move.MovingPieceColor.OppositeColor());
        }

        if (move.PutsOpponentInCheckmate)
        {
            Status = move.MovingPieceColor == Enums.Color.Light
                ? Enums.GameStatus.CheckmateByLight
                : Enums.GameStatus.CheckmateByDark;
        }
    }

    private void EndCurrentPlayerTurn()
    {
        // Deselect square/piece that moved
        SelectedSquare = null;
        ValidDestinationsForSelectedPiece.Clear();
        Board.ClearValidDestinations();

        CurrentPlayerColor = CurrentPlayerColor.OppositeColor();
        MoveCompleted?.Invoke(this, EventArgs.Empty);
    }

    #endregion
}