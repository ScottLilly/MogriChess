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
    public IEnumerable<Move> LegalMovesForCurrentPlayer;
    private Enums.GameStatus _status = Enums.GameStatus.Preparing;
    private Square _selectedSquare;

    public Enums.Color CurrentPlayerColor
    {
        get => _currentPlayerColor;
        set
        {
            _currentPlayerColor = value;

            if (_currentPlayerColor == Enums.Color.NotSelected)
            {
                return;
            }

            CacheLegalMovesForCurrentPlayer();

            if (LegalMovesForCurrentPlayer.None())
            {
                Status = Enums.GameStatus.Stalemate;
            }
        }
    }

    public Enums.GameStatus Status
    {
        get => _status;
        set
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

    public event EventHandler<GameEndedEventArgs> GameEnded;
    public event PropertyChangedEventHandler PropertyChanged;

    public Game(Board board)
    {
        Board = board;
    }

    public bool PlayerIsInCheckmate(Enums.Color playerColor) =>
        Board.SquaresWithPiecesOfColor(playerColor)
            .All(square => Board.PotentialMovesForPieceAt(square)
                .None(move => MoveGetsKingOutOfCheck(playerColor, move)));

    private bool MoveGetsKingOutOfCheck(Enums.Color kingColor, Move potentialMove)
    {
        return Board.GetSimulatedMoveResult(potentialMove,
            () => Board.KingCannotBeCaptured(kingColor));
    }

    public void CacheLegalMovesForCurrentPlayer() =>
        LegalMovesForCurrentPlayer =
            Board.LegalMovesForPlayer(_currentPlayerColor);
}