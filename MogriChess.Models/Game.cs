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
    private Enums.Color _currentPlayerColor = Enums.Color.Light;
    private IEnumerable<Move> _legalMovesForCurrentPlayer;

    public Enums.Color CurrentPlayerColor
    {
        get => _currentPlayerColor;
        private set
        {
            _currentPlayerColor = value;

            CacheLegalMovesForCurrentPlayer();

            if (_legalMovesForCurrentPlayer.None())
            {
                HandleStalemate();
            }
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
        if (SelectedSquare != null)
        {
            SelectedSquare.IsSelected = false;
            SelectedSquare = null;
        }

        Board.ClearValidDestinations();
        MoveHistory.Clear();

        CurrentPlayerColor = Enums.Color.Light;
    }

    public void SelectSquare(Square square)
    {
        // No square is currently selected
        if (SelectedSquare == null)
        {
            // No piece is on square, so return
            if (square.IsEmpty)
            {
                return;
            }

            SelectMoveOriginationSquare(square);

            return;
        }

        // If the player selected the currently-selected square, de-select it.
        if (SelectedSquare == square)
        {
            DeselectSelectedSquare();

            return;
        }

        // Move the piece to the second selected square, if valid
        MoveToSelectedSquare(square);
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
        if (MoveHistory.Last().PutsOpponentInCheckmate)
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

        if (move.PutsOpponentInCheckmate)
        {
            HandleCheckmate();
        }
    }

    private void DetermineIfMovePutsOpponentInCheckOrCheckmate(Move move)
    {
        if (Board.KingCanBeCaptured(move.MovingPieceColor.OppositeColor()))
        {
            move.PutsOpponentInCheck = true;
            move.PutsOpponentInCheckmate =
                Board.PlayerIsInCheckmate(move.MovingPieceColor.OppositeColor());
        }
    }

    private void DeselectSelectedSquare()
    {
        SelectedSquare.IsSelected = false;
        SelectedSquare = null;
    }

    private void HandleCheckmate() =>
        GameEnded?.Invoke(this,
            new GameEndedEventArgs(MoveHistory.Last().MovingPieceColor == Enums.Color.Light
                ? Enums.GameStatus.CheckmateByLight
                : Enums.GameStatus.CheckmateByDark));

    private void HandleStalemate() =>
        GameEnded?.Invoke(this, new GameEndedEventArgs(Enums.GameStatus.Stalemate));

    private void EndCurrentPlayerTurn()
    {
        CurrentPlayerColor = CurrentPlayerColor.OppositeColor();
        MoveCompleted?.Invoke(this, EventArgs.Empty);
    }

    #endregion
}