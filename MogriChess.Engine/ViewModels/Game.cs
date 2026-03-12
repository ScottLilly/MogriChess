using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MogriChess.Core;
using MogriChess.Models;
using MogriChess.Models.CustomEventArgs;
using MogriChess.Services;

namespace MogriChess.ViewModels;

public class Game(GameConfig gameConfig = null) : ObservableObject
{
    public const int MAX_MOVES_WITHOUT_CAPTURE = 50;

    private bool _displayValidDestinations = true;
    private Enums.Color _currentPlayerColor = Enums.Color.NotSelected;
    private Enums.GameStatus _status = Enums.GameStatus.Preparing;
    private Square _selectedSquare;
    private IEnumerable<Move> _legalMovesForCurrentPlayer;

    private Enums.GameStatus Status
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
                else if (value == Enums.GameStatus.DrawNoCaptures)
                {
                    GameEnded?.Invoke(this, new GameEndedEventArgs(Enums.GameStatus.DrawNoCaptures));
                }
            }

            _status = value;
        }
    }
    private BotPlayer LightPlayerBot { get; set; }
    private BotPlayer DarkPlayerBot { get; set; }
    private Square SelectedSquare
    {
        get => _selectedSquare;
        set
        {
            SelectedSquare?.IsSelected = false;

            _selectedSquare = value;

            SelectedSquare?.IsSelected = true;
        }
    }
    private ObservableCollection<Move> ValidDestinationsForSelectedPiece { get; } = [];

    public Enums.Color CurrentPlayerColor
    {
        get => _currentPlayerColor;
        set
        {
            if (!SetProperty(ref _currentPlayerColor, value))
            {
                return;
            }

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
    public bool DisplayRankFileLabels { get; set; } = true;
    public bool DisplayValidDestinations
    {
        get => _displayValidDestinations;
        set
        {
            if (!SetProperty(ref _displayValidDestinations, value))
            {
                return;
            }

            SetValidDestinations();
        }
    }
    public string SelectedSquareColor { get; private set; } = gameConfig?.SelectedSquareColor ?? "";
    public string ValidDestinationSquareColor { get; private set; } = gameConfig?.ValidDestinationSquareColor ?? "";
    public Board Board { get; } = BoardFactory.GetNewGameBoard(gameConfig);

    public ObservableCollection<MoveStruct> MoveHistory { get; } =
        [];

    public event EventHandler<GameEndedEventArgs> GameEnded;

    public void StartGame(Enums.PlayerType lightPlayer = Enums.PlayerType.Human,
        Enums.PlayerType darkPlayer = Enums.PlayerType.Human)
    {
        LightPlayerBot =
            lightPlayer == Enums.PlayerType.Bot
                ? new BotPlayer(Enums.Color.Light,
                    new PieceValueCalculator(
                        new PieceValueCalculatorGenome(1,2,5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 999)))
                : null;

        DarkPlayerBot =
            darkPlayer == Enums.PlayerType.Bot
                ? new BotPlayer(Enums.Color.Dark,
                    new PieceValueCalculator(
                        new PieceValueCalculatorGenome(1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 999)))
                : null;

        BoardFactory.PopulateBoardWithStartingPieces(Board);
        BeginGame();

        if (LightPlayerBot != null)
        {
            MakeBotMove();
        }
    }

    public void SelectSquare(Square square)
    {
        // If square doesn't have a piece, return
        // If piece is not current player's color, return
        if (SelectedSquare == null &&
            (square.Piece == null ||
             square.Piece.Color != CurrentPlayerColor))
        {
            return;
        }

        // If SelectedSquare == null, select the square
        if (SelectedSquare == null)
        {
            SelectedSquare = square;
            SetValidDestinations();

            return;
        }

        // If passed-in square is the SelectedSquare, unselect it
        if (SelectedSquare == square)
        {
            SelectedSquare = null;
            ClearValidDestinations();

            return;
        }

        // If SelectedSquare != null:
        // If DestinationSquare is in ValidDestinations, perform move
        // otherwise, do nothing
        if (ValidDestinationsForSelectedPiece.Any(m =>
                m.DestinationSquare.SquareShorthand == square.SquareShorthand))
        {
            MoveToSelectedSquare(square);
        }
    }

    public string GetSerializedGameState()
    {
        return BoardStateService.GetSerializedGameState(this);
    }

    public string GetSerializedMoveHistory()
    {
        return BoardStateService.GetSerializedMoveHistory(this);
    }

    #region Private methods

    private void BeginGame()
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

    private void MoveToSelectedSquare(Square square)
    {
        // Check that the destination square is a valid move
        Move move =
            ValidDestinationsForSelectedPiece.FirstOrDefault(d =>
                d.DestinationSquare.SquareShorthand == square.SquareShorthand);

        if (move == null)
        {
            return;
        }

        bool wasPawnMove = SelectedSquare?.Piece?.PieceType == Enums.PieceType.Pawn;

        Board.MovePiece(SelectedSquare, square);

        DetermineIfMovePutsOpponentInCheckOrCheckmate(move);

        bool moveResetsDrawCounter = move.IsCapturingMove || wasPawnMove;

        if (!moveResetsDrawCounter &&
            MoveHistory.Count >= (MAX_MOVES_WITHOUT_CAPTURE - 1) &&
            MoveHistory.TakeLast(MAX_MOVES_WITHOUT_CAPTURE - 1)
                .All(m => !m.IsCapture && !m.IsPawnMove))
        {
            move.IsDrawFromMaxMoves = true;
        }

        MoveHistory.Add(new MoveStruct(
            move.MovingPieceColor.ToString(),
            move.MoveShorthand,
            move.MoveResult,
            move.IsCapturingMove,
            wasPawnMove));

        EndCurrentPlayerTurn(move);
    }

    private void DetermineIfMovePutsOpponentInCheckOrCheckmate(Move move)
    {
        if (Board.KingCanBeCaptured(move.MovingPieceColor.OppositeColor()))
        {
            move.PutsOpponentInCheck = true;
            move.PutsOpponentInCheckmate =
                GameEngine.IsPlayerInCheckmate(Board, move.MovingPieceColor.OppositeColor());
        }
    }

    private void EndCurrentPlayerTurn(Move move)
    {
        // Deselect square/piece that moved
        SelectedSquare = null;
        ValidDestinationsForSelectedPiece.Clear();
        Board.ClearValidDestinations();

        if (move.PutsOpponentInCheckmate)
        {
            Status = move.MovingPieceColor == Enums.Color.Light
                ? Enums.GameStatus.CheckmateByLight
                : Enums.GameStatus.CheckmateByDark;
        }
        else if (move.IsDrawFromMaxMoves)
        {
            Status = Enums.GameStatus.DrawNoCaptures;
        }

        if (Status != Enums.GameStatus.Playing)
        {
            // Game has ended
            return;
        }

        CurrentPlayerColor = CurrentPlayerColor.OppositeColor();

        MakeBotMove();
    }

    private void SetValidDestinations()
    {
        ClearValidDestinations();

        foreach (Move move in LegalMovesForSelectedPiece())
        {
            ValidDestinationsForSelectedPiece.Add(move);
            move.DestinationSquare.IsValidDestination = DisplayValidDestinations;
        }
    }

    private List<Move> LegalMovesForSelectedPiece()
    {
        if (_legalMovesForCurrentPlayer == null)
        {
            CacheLegalMovesForCurrentPlayer();
        }

        if (SelectedSquare == null)
        {
            return [];
        }

        return _legalMovesForCurrentPlayer
            .Where(m => m.OriginationSquare.SquareShorthand == SelectedSquare.SquareShorthand)
            .ToList();
    }

    private void ClearValidDestinations()
    {
        ValidDestinationsForSelectedPiece.Clear();
        Board.ClearValidDestinations();
    }

    private void CacheLegalMovesForCurrentPlayer() =>
        _legalMovesForCurrentPlayer =
            GameEngine.GetLegalMovesForPlayer(Board, _currentPlayerColor);

    private async void MakeBotMove(BotPlayer botPlayer)
    {
        if (Status != Enums.GameStatus.Playing)
        {
            return;
        }

        Move bestMove = botPlayer.FindBestMove(Board);

        SelectSquare(bestMove.OriginationSquare);

        // Only show best move for destination
        ClearValidDestinations();
        ValidDestinationsForSelectedPiece.Add(bestMove);
        bestMove.DestinationSquare.IsValidDestination = true;

        await Task.Delay(750);

        SelectSquare(bestMove.DestinationSquare);
    }

    private void MakeBotMove()
    {
        if (CurrentPlayerColor == Enums.Color.Dark &&
            DarkPlayerBot != null)
        {
            MakeBotMove(DarkPlayerBot);
        }

        if (CurrentPlayerColor == Enums.Color.Light &&
            LightPlayerBot != null)
        {
            MakeBotMove(LightPlayerBot);
        }
    }

    #endregion
}