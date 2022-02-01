using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using MogriChess.Core;

namespace MogriChess.Models;

public class Board : INotifyPropertyChanged
{
    public ColorScheme BoardColorScheme { get; }
    public ColorScheme PieceColorScheme { get; }

    public ObservableCollection<Square> Squares { get; } =
        new ObservableCollection<Square>();

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
        RemovePiecesFromAllSquares();

        foreach (PiecePlacement placement in piecePlacements)
        {
            PlacePieceOnSquare(placement.Piece, SquareAt(placement.Rank, placement.File));
        }
    }

    public void MovePiece(Square originationSquare, Square destinationSquare)
    {
        PlacePieceOnSquare(originationSquare.Piece, destinationSquare);
        originationSquare.Piece = null;
    }

    public List<Move> PotentialMovesForPieceAt(int rank, int file)
    {
        Square originationSquare = SquareAt(rank, file);

        List<Move> validMoves = new List<Move>();

        if (originationSquare.Piece == null)
        {
            return validMoves;
        }

        validMoves.AddRange(ValidMovesInDirection(originationSquare, Enums.Direction.Forward));
        validMoves.AddRange(ValidMovesInDirection(originationSquare, Enums.Direction.ForwardRight));
        validMoves.AddRange(ValidMovesInDirection(originationSquare, Enums.Direction.Right));
        validMoves.AddRange(ValidMovesInDirection(originationSquare, Enums.Direction.BackRight));
        validMoves.AddRange(ValidMovesInDirection(originationSquare, Enums.Direction.Back));
        validMoves.AddRange(ValidMovesInDirection(originationSquare, Enums.Direction.BackLeft));
        validMoves.AddRange(ValidMovesInDirection(originationSquare, Enums.Direction.Left));
        validMoves.AddRange(ValidMovesInDirection(originationSquare, Enums.Direction.ForwardLeft));

        return validMoves;
    }

    public T GetSimulatedMoveResult<T>(Move move, Func<T> func)
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

    public List<Move> LegalMovesForPlayer(Enums.Color playerColor) =>
        SquaresWithPiecesOfColor(playerColor)
            .SelectMany(square => PotentialMovesForPieceAt(square.Rank, square.File))
            .Where(move => GetSimulatedMoveResult(move, () => KingCannotBeCaptured(playerColor)))
            .ToList();

    public bool KingCannotBeCaptured(Enums.Color playerColor) =>
        !KingCanBeCaptured(playerColor);
      
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

    internal void ClearValidDestinations() =>
        Squares.ApplyToEach(s => s.IsValidDestination = false);

    internal IEnumerable<Square> SquaresWithPiecesOfColor(Enums.Color color) =>
        Squares.Where(s => s.Piece?.Color == color);

    internal Square SquareAt(int rank, int file) =>
        Squares.First(s => s.Rank.Equals(rank) && s.File.Equals(file));

    #endregion

    #region Private methods

    private void PopulateBoardWithSquares()
    {
        for (int rank = 1; rank <= Constants.NumberOfRanks; rank++)
        {
            for (int file = 1; file <= Constants.NumberOfFiles; file++)
            {
                Squares.Add(new Square(rank, file, BoardColorScheme));
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

    private IEnumerable<Move> ValidMovesInDirection(Square originationSquare, Enums.Direction direction)
    {
        List<Move> validMoves = new List<Move>();

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

            Square destinationSquare = SquareAt(destinationRank, destinationFile);
            Move potentialMove = new Move(originationSquare, destinationSquare);

            // Un-promoted pawn reached opponent's back rank, and needs to be promoted.
            potentialMove.IsPromotingMove =
                IsPawnPromotionMove(movingPiece, destinationSquare);

            if (destinationSquare.IsEmpty)
            {
                validMoves.Add(potentialMove);
            }
            else
            {
                if (destinationSquare.Piece.Color != movingPiece.Color)
                {
                    // Square is occupied by an opponent's piece
                    potentialMove.IsCapturingMove = true;
                    potentialMove.PutsOpponentInCheck =
                        destinationSquare.Piece.IsKing;

                    validMoves.Add(potentialMove);
                }

                break;
            }
        }

        return validMoves;
    }

    private bool MoveGetsKingOutOfCheck(Enums.Color kingColor, Move potentialMove)
    {
        return GetSimulatedMoveResult(potentialMove,
            () => KingCannotBeCaptured(kingColor));
    }

    private IEnumerable<Move> PotentialMovesForPieceAt(Square square) =>
        PotentialMovesForPieceAt(square.Rank, square.File);

    private static bool IsPawnPromotionMove(Piece movingPiece, Square destinationSquare) =>
        movingPiece.IsUnpromotedPawn &&
        ((movingPiece.Color == Enums.Color.Light && destinationSquare.Rank == Constants.BackRankDark) ||
         (movingPiece.Color == Enums.Color.Dark && destinationSquare.Rank == Constants.BackRankLight));

    private void RemovePiecesFromAllSquares() =>
        Squares.ApplyToEach(s => s.Piece = null);

    #endregion
}