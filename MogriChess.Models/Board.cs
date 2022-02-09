using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MogriChess.Core;

namespace MogriChess.Models;

public class Board : INotifyPropertyChanged
{
    public ColorScheme BoardColorScheme { get; }
    public ColorScheme PieceColorScheme { get; }

    public ObservableDictionary<string, Square> Squares { get; } =
        new ObservableDictionary<string, Square>();

    public event PropertyChangedEventHandler PropertyChanged;

    public Board(ColorScheme boardColorScheme, ColorScheme piecesColorScheme)
    {
        BoardColorScheme = boardColorScheme;
        PieceColorScheme = piecesColorScheme;

        PopulateBoardWithSquares();
    }

    #region Public methods

    public void PlaceStartingPieces(List<PiecePlacement> piecePlacements)
    {
        foreach (Square square in Squares.Values)
        {
            square.Piece = null;
        }

        foreach (PiecePlacement placement in piecePlacements)
        {
            PlacePieceOnSquare(placement.Piece, Squares[placement.Shorthand]);
        }
    }

    public IEnumerable<Move> LegalMovesForPieceAt(int rank, int file) =>
        PotentialMovesForPieceAt(ModelFunctions.GetShorthand(rank, file))
            .Where(m => GetSimulatedMoveResult(m, () => KingCannotBeCaptured(m.MovingPieceColor)));

    public bool KingCanBeCaptured(Enums.Color playerColor) =>
        SquaresWithPiecesOfColor(playerColor.OppositeColor())
            .Any(square => PotentialMovesForPieceAt(square)
                .Any(m => m.PutsOpponentInCheck));

    public bool PlayerIsInCheckmate(Enums.Color playerColor) =>
        SquaresWithPiecesOfColor(playerColor)
            .All(square => PotentialMovesForPieceAt(square)
                .None(move => MoveGetsKingOutOfCheck(playerColor, move)));

    #endregion

    #region Internal methods

    public void ClearValidDestinations() =>
        Squares.Values.ApplyToEach(s => s.IsValidDestination = false);

    internal IEnumerable<Square> SquaresWithPiecesOfColor(Enums.Color color) =>
        Squares.Values.Where(s => s.Piece?.Color == color);

    public void MovePiece(Square originationSquare, Square destinationSquare)
    {
        PlacePieceOnSquare(originationSquare.Piece, destinationSquare);
        originationSquare.Piece = null;
    }

    internal List<Move> LegalMovesForPlayer(Enums.Color playerColor) =>
        SquaresWithPiecesOfColor(playerColor)
            .SelectMany(square => LegalMovesForPieceAt(square.Rank, square.File))
            .ToList();

    internal T GetSimulatedMoveResult<T>(Move move, Func<T> func)
    {
        // Clone pieces pre-move
        Piece originalMovingPiece = move.OriginationSquare.Piece.Clone();
        Piece destinationPiece = move.DestinationSquare.Piece?.Clone();

        // Make simulated move
        MovePiece(move.OriginationSquare, move.DestinationSquare);

        // Run passed-in function
        T result = func.Invoke();

        // Revert simulated move
        move.OriginationSquare.Piece = originalMovingPiece;
        move.DestinationSquare.Piece = destinationPiece;

        return result;
    }

    #endregion

    #region Private methods

    private void PopulateBoardWithSquares()
    {
        for (int rank = 1; rank <= Constants.NumberOfRanks; rank++)
        {
            for (int file = 1; file <= Constants.NumberOfFiles; file++)
            {
                Square square = new Square(rank, file, BoardColorScheme);

                Squares.Add(square.SquareShorthand, square);
            }
        }
    }

    private static void PlacePieceOnSquare(Piece piece, Square destinationSquare)
    {
        if (destinationSquare.Piece != null)
        {
            piece.CapturePiece(destinationSquare.Piece);
        }

        destinationSquare.Piece = piece;

        if (IsPawnPromotionMove(piece, destinationSquare))
        {
            piece.Promote();
        }
    }

    private List<Move> PotentialMovesForPieceAt(Square square) =>
        PotentialMovesForPieceAt(square.SquareShorthand);

    private List<Move> PotentialMovesForPieceAt(string squareShorthand)
    {
        Square originationSquare = Squares[squareShorthand];

        List<Move> validMoves = new List<Move>();

        if (originationSquare.Piece == null)
        {
            return validMoves;
        }

        validMoves.AddRange(PotentialMovesInDirection(originationSquare, Enums.Direction.Forward));
        validMoves.AddRange(PotentialMovesInDirection(originationSquare, Enums.Direction.ForwardRight));
        validMoves.AddRange(PotentialMovesInDirection(originationSquare, Enums.Direction.Right));
        validMoves.AddRange(PotentialMovesInDirection(originationSquare, Enums.Direction.BackRight));
        validMoves.AddRange(PotentialMovesInDirection(originationSquare, Enums.Direction.Back));
        validMoves.AddRange(PotentialMovesInDirection(originationSquare, Enums.Direction.BackLeft));
        validMoves.AddRange(PotentialMovesInDirection(originationSquare, Enums.Direction.Left));
        validMoves.AddRange(PotentialMovesInDirection(originationSquare, Enums.Direction.ForwardLeft));

        return validMoves;
    }

    private List<Move> PotentialMovesInDirection(Square originationSquare,
        Enums.Direction direction)
    {
        List<Move> potentialMoves = new List<Move>();

        Piece movingPiece = originationSquare.Piece;

        int maxMovementSquareForDirection =
            movingPiece.MaxMovementSquaresForDirection(direction);
        (int rankMultiplier, int fileMultiplier) =
            movingPiece.MovementMultipliersForDirection(direction);

        for (int i = 1; i <= maxMovementSquareForDirection; i++)
        {
            int destinationRank = originationSquare.Rank + (i * rankMultiplier);
            int destinationFile = originationSquare.File + (i * fileMultiplier);

            // Off board, stop checking
            if (destinationRank is < 1 or > Constants.NumberOfRanks ||
                destinationFile is < 1 or > Constants.NumberOfFiles)
            {
                break;
            }

            var destSquareShorthand = ModelFunctions.GetShorthand(destinationRank, destinationFile);
            Square destinationSquare = Squares[destSquareShorthand];
            Move potentialMove = new Move(originationSquare, destinationSquare);

            // Un-promoted pawn reached opponent's back rank, and needs to be promoted.
            potentialMove.IsPromotingMove =
                IsPawnPromotionMove(movingPiece, destinationSquare);

            if (destinationSquare.IsEmpty)
            {
                potentialMoves.Add(potentialMove);
            }
            else
            {
                if (destinationSquare.Piece.Color != movingPiece.Color)
                {
                    // Square is occupied by an opponent's piece
                    potentialMove.IsCapturingMove = true;
                    potentialMove.PutsOpponentInCheck =
                        destinationSquare.Piece.IsKing;

                    potentialMoves.Add(potentialMove);
                }

                break;
            }
        }

        return potentialMoves;
    }

    private bool KingCannotBeCaptured(Enums.Color playerColor) =>
        !KingCanBeCaptured(playerColor);

    private bool MoveGetsKingOutOfCheck(Enums.Color kingColor, Move potentialMove)
    {
        return GetSimulatedMoveResult(potentialMove,
            () => KingCannotBeCaptured(kingColor));
    }

    private static bool IsPawnPromotionMove(Piece movingPiece, Square destinationSquare) =>
        movingPiece.IsUnpromotedPawn &&
        ((movingPiece.Color == Enums.Color.Light && destinationSquare.Rank == Constants.BackRankDark) ||
         (movingPiece.Color == Enums.Color.Dark && destinationSquare.Rank == Constants.BackRankLight));

    #endregion
}