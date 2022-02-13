using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
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

            SetValidDestinations();
        }
    }

    public PlaySession()
    {
        CurrentGame = GameFactory.GetNewGame();
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
        BeginGame();

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
            SetValidDestinations();

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
            MoveToSelectedSquare(square);
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

    private void BeginGame()
    {
        // Clear out game
        CurrentGame.CurrentPlayerColor = Enums.Color.NotSelected;
        CurrentGame.SelectedSquare = null;
        CurrentGame.Board.ClearValidDestinations();
        CurrentGame.MoveHistory.Clear();

        // Start game
        CurrentGame.CurrentPlayerColor = Enums.Color.Light;
        CurrentGame.Status = Enums.GameStatus.Playing;
    }

    private void MoveToSelectedSquare(Square square)
    {
        // Check that the destination square is a valid move
        Move move =
            CurrentGame.ValidDestinationsForSelectedPiece.FirstOrDefault(d =>
                d.DestinationSquare.SquareShorthand == square.SquareShorthand);

        if (move == null)
        {
            return;
        }

        CurrentGame.Board.MovePiece(CurrentGame.SelectedSquare, square);

        DetermineIfMovePutsOpponentInCheckOrCheckmate(move);

        CurrentGame.MoveHistory.Add(move);

        EndCurrentPlayerTurn();
    }

    private void DetermineIfMovePutsOpponentInCheckOrCheckmate(Move move)
    {
        if (CurrentGame.Board.KingCanBeCaptured(move.MovingPieceColor.OppositeColor()))
        {
            move.PutsOpponentInCheck = true;
            move.PutsOpponentInCheckmate =
                CurrentGame.PlayerIsInCheckmate(move.MovingPieceColor.OppositeColor());
        }

        if (move.PutsOpponentInCheckmate)
        {
            CurrentGame.Status = move.MovingPieceColor == Enums.Color.Light
                ? Enums.GameStatus.CheckmateByLight
                : Enums.GameStatus.CheckmateByDark;
        }
    }

    private void EndCurrentPlayerTurn()
    {
        // Deselect square/piece that moved
        CurrentGame.SelectedSquare = null;
        CurrentGame.ValidDestinationsForSelectedPiece.Clear();
        CurrentGame.Board.ClearValidDestinations();

        CurrentGame.CurrentPlayerColor = CurrentGame.CurrentPlayerColor.OppositeColor();

        if (CurrentGame.Status != Enums.GameStatus.Playing)
        {
            // Game has ended
            return;
        }

        MakeBotMove();
    }

    private void SetValidDestinations()
    {
        ClearValidDestinations();

        foreach (Move move in LegalMovesForSelectedPiece())
        {
            CurrentGame.ValidDestinationsForSelectedPiece.Add(move);
            move.DestinationSquare.IsValidDestination = DisplayValidDestinations;
        }
    }

    private List<Move> LegalMovesForSelectedPiece()
    {
        if (CurrentGame.LegalMovesForCurrentPlayer == null)
        {
            CurrentGame.CacheLegalMovesForCurrentPlayer();
        }

        if (CurrentGame.SelectedSquare == null)
        {
            return new List<Move>();
        }

        return CurrentGame.LegalMovesForCurrentPlayer
            .Where(m => m.OriginationSquare.SquareShorthand == CurrentGame.SelectedSquare.SquareShorthand)
            .ToList();
    }

    private void ClearValidDestinations()
    {
        CurrentGame.ValidDestinationsForSelectedPiece.Clear();
        CurrentGame.Board.ClearValidDestinations();
    }

    private async void MakeBotMove(BotPlayer botPlayer)
    {
        if (CurrentGame.Status != Enums.GameStatus.Playing)
        {
            return;
        }

        Move bestMove = botPlayer.FindBestMove(CurrentGame.Board);

        SelectSquare(bestMove.OriginationSquare);

        // Only show best move for destination
        ClearValidDestinations();
        CurrentGame.ValidDestinationsForSelectedPiece.Add(bestMove);
        bestMove.DestinationSquare.IsValidDestination = true;

        await Task.Delay(750);

        SelectSquare(bestMove.DestinationSquare);
    }

    private void MakeBotMove()
    {
        if (CurrentGame.CurrentPlayerColor == Enums.Color.Dark &&
            CurrentGame.DarkPlayerBot != null)
        {
            MakeBotMove(CurrentGame.DarkPlayerBot);
        }

        if (CurrentGame.CurrentPlayerColor == Enums.Color.Light &&
            CurrentGame.LightPlayerBot != null)
        {
            MakeBotMove(CurrentGame.LightPlayerBot);
        }
    }

    #endregion
}