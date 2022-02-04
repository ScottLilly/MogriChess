using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MogriChess.Core;
using MogriChess.Models.CustomEventArgs;

namespace MogriChess.Models;

public class Game : INotifyPropertyChanged
{
    private Square _selectedSquare;
    private bool _displayValidDestinations = true;
    private Enums.Color _currentPlayerColor = Enums.Color.NotSelected;
    private IEnumerable<Move> _legalMovesForCurrentPlayer;
    private Enums.GameStatus _status = Enums.GameStatus.Preparing;

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

    public bool DisplayValidDestinations
    {
        get => _displayValidDestinations;
        set
        {
            _displayValidDestinations = value;

            if (_displayValidDestinations)
            {
                ValidDestinationsForSelectedPiece
                    .ApplyToEach(d => d.DestinationSquare.IsValidDestination = true);
            }
            else
            {
                Board.ClearValidDestinations();
            }
        }
    }

    public BotPlayer LightPlayerBot { get; set; }
    public BotPlayer DarkPlayerBot { get; set; }

    private Square SelectedSquare
    {
        get => _selectedSquare;
        set
        {
            _selectedSquare = value;

            ValidDestinationsForSelectedPiece.Clear();

            Board.ClearValidDestinations();

            if (SelectedSquare == null)
            {
                return;
            }

            List<Move> legalMovesForSelectedPiece =
                LegalMovesForSelectedPiece();

            legalMovesForSelectedPiece.ApplyToEach(lm => ValidDestinationsForSelectedPiece.Add(lm));

            if (DisplayValidDestinations)
            {
                legalMovesForSelectedPiece.ApplyToEach(lm => lm.DestinationSquare.IsValidDestination = true);
            }
        }
    }

    private ObservableCollection<Move> ValidDestinationsForSelectedPiece { get; } =
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
        CurrentPlayerColor = Enums.Color.NotSelected;

        if (SelectedSquare != null)
        {
            SelectedSquare.IsSelected = false;
            SelectedSquare = null;
        }

        Board.ClearValidDestinations();
        MoveHistory.Clear();

        CurrentPlayerColor = Enums.Color.Light;
        Status = Enums.GameStatus.Playing;
    }

    public void SelectSquare(Square square)
    {
        if (SelectedSquare == null)
        {
            // No piece is on square, so return
            if (square.IsEmpty)
            {
                return;
            }

            SelectMoveOriginationSquare(square);
        }
        else if (SelectedSquare == square)
        {
            // The player selected the currently-selected square, de-select it.
            DeselectSelectedSquare();
        }
        else
        {
            // Move the piece to the second selected square, if valid
            MoveToSelectedSquare(square);
        }
    }

    private void SelectMoveOriginationSquare(Square square)
    {
        // The square contains a piece of the current player's color.
        // So, it's a valid selection.
        if (square.Piece.Color == CurrentPlayerColor)
        {
            SelectedSquare = square;
            SelectedSquare.IsSelected = true;
        }
    }

    public async void MakeBotMove(BotPlayer botPlayer)
    {
        if (Status != Enums.GameStatus.Playing)
        {
            return;
        }

        Move bestMove = botPlayer.FindBestMove(Board);

        SelectSquare(bestMove.OriginationSquare);

        ValidDestinationsForSelectedPiece.ApplyToEach(d =>
            d.DestinationSquare.IsValidDestination = false);
        ValidDestinationsForSelectedPiece.Clear();

        ValidDestinationsForSelectedPiece.Add(bestMove);

        if (DisplayValidDestinations)
        {
            ValidDestinationsForSelectedPiece.ApplyToEach(d =>
                d.DestinationSquare.IsValidDestination = true);
        }

        await Task.Delay(750);

        SelectSquare(bestMove.DestinationSquare);
    }

    #endregion

    #region Private methods

    private void CacheLegalMovesForCurrentPlayer() =>
        _legalMovesForCurrentPlayer =
            Board.LegalMovesForPlayer(_currentPlayerColor);

    private List<Move> LegalMovesForSelectedPiece()
    {
        if (_legalMovesForCurrentPlayer == null)
        {
            CacheLegalMovesForCurrentPlayer();
        }

        return _legalMovesForCurrentPlayer
            .Where(m => m.OriginationSquare.SquareShorthand.Equals(SelectedSquare.SquareShorthand))
            .ToList();
    }

    private void MoveToSelectedSquare(Square square)
    {
        // Check that the destination square is a valid move
        Move move =
            ValidDestinationsForSelectedPiece.FirstOrDefault(d =>
                d.DestinationRank == square.Rank &&
                d.DestinationFile == square.File);

        if (move == null)
        {
            return;
        }

        Board.MovePiece(SelectedSquare, square);

        DeselectSelectedSquare();

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

    private void DeselectSelectedSquare()
    {
        SelectedSquare.IsSelected = false;
        SelectedSquare = null;
    }

    private void EndCurrentPlayerTurn()
    {
        CurrentPlayerColor = CurrentPlayerColor.OppositeColor();
        MoveCompleted?.Invoke(this, EventArgs.Empty);
    }

    #endregion
}