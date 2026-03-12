using System;
using System.Collections.Generic;
using System.Linq;
using MogriChess.Engine.Core;

namespace MogriChess.Engine.Models;

public class Board : ObservableObject
{
    private readonly Square[,] _squaresByPosition =
        new Square[Constants.NumberOfRanks, Constants.NumberOfFiles];

    private ColorScheme _boardColorScheme;

    public ColorScheme BoardColorScheme
    {
        get => _boardColorScheme;
        set => SetProperty(ref _boardColorScheme, value);
    }
    public ColorScheme PieceColorScheme { get; }

    public ObservableDictionary<string, Square> Squares { get; } =
        [];

    public Board(ColorScheme boardColorScheme, ColorScheme piecesColorScheme)
    {
        BoardColorScheme = boardColorScheme;
        PieceColorScheme = piecesColorScheme;

        PopulateBoardWithSquares();
    }

    #region Public methods

    /// <summary>
    /// Generates all pseudo-legal moves for the piece at the specified rank/file.
    /// Pseudo-legal moves respect piece movement and occupancy, but do not consider king safety.
    /// </summary>
    public IEnumerable<Move> GeneratePseudoLegalMovesForPieceAt(int rank, int file) =>
        MoveGenerator.GeneratePseudoLegalMovesForPieceAt(this, GetSquareAt(rank, file));

    public void MovePiece(Square originationSquare, Square destinationSquare)
    {
        PlacePieceOnSquare(originationSquare.Piece, destinationSquare);
        originationSquare.Piece = null;
    }

    public bool IsKingInCheck(Color playerColor) =>
        SquaresWithPiecesOfColor(playerColor.OppositeColor())
            .Any(square => MoveGenerator.GeneratePseudoLegalMovesForPieceAt(this, square)
                .Any(m => m.PutsOpponentInCheck));

    public void ClearValidDestinations() =>
        Squares.Values.ApplyToEach(s => s.IsValidDestination = false);

    public IEnumerable<Square> SquaresWithPiecesOfColor(Color color) =>
        Squares.Values.Where(s => s.Piece?.Color == color);

    public List<Move> LegalMovesForPlayer(Color playerColor) =>
        GameEngine.GetLegalMovesForPlayer(this, playerColor).ToList();

    public T GetSimulatedMoveResult<T>(Move move, Func<T> func)
    {
        // Clone pieces pre-move
        Piece originalMovingPiece = ClonePiece(move.OriginationSquare.Piece);
        Piece destinationPiece = ClonePiece(move.DestinationSquare.Piece);

        // Make simulated move
        MovePiece(move.OriginationSquare, move.DestinationSquare);

        // Run passed-in function
        T result = func.Invoke();

        // Revert simulated move
        move.OriginationSquare.Piece = originalMovingPiece;
        move.DestinationSquare.Piece = destinationPiece;

        return result;
    }

    public void PlacePieceOnSquare(Piece piece, Square destinationSquare)
    {
        if (destinationSquare.Piece != null)
        {
            piece = CapturePiece(piece, destinationSquare.Piece);
        }

        destinationSquare.Piece = piece;

        if (IsPawnPromotionMove(piece, destinationSquare))
        {
            destinationSquare.Piece = Promote(piece);
        }
    }

    /// <summary>
    /// Generates all pseudo-legal moves for the given square.
    /// Pseudo-legal moves respect piece movement and occupancy, but do not consider king safety.
    /// </summary>
    public List<Move> GeneratePseudoLegalMovesForPieceAt(Square square) =>
        MoveGenerator.GeneratePseudoLegalMovesForPieceAt(this, square);

    public bool IsKingSafe(Color playerColor) =>
        !IsKingInCheck(playerColor);

    #endregion

    #region Private methods

    private void PopulateBoardWithSquares()
    {
        for (int rank = 1; rank <= Constants.NumberOfRanks; rank++)
        {
            for (int file = 1; file <= Constants.NumberOfFiles; file++)
            {
                Square square = new Square(rank, file);

                Squares.Add(square.SquareShorthand, square);
                _squaresByPosition[rank - 1, file - 1] = square;
            }
        }
    }

    internal Square GetSquareAt(int rank, int file) =>
        _squaresByPosition[rank - 1, file - 1];

    private Piece ClonePiece(Piece piece)
    {
        if (piece == null)
        {
            return null;
        }

        return new Piece(piece.ColorScheme, piece.Color, piece.PieceType,
            piece.Forward, piece.ForwardRight,
            piece.Right, piece.BackRight,
            piece.Back, piece.BackLeft,
            piece.Left, piece.ForwardLeft);
    }

    private static Piece CapturePiece(Piece movingPiece, Piece capturedPiece)
    {
        // Kings do not acquire the movement abilities of pieces they capture
        if(movingPiece.IsKing)
        {
            return movingPiece;
        }

        return CombinePieceMovementAbilities(movingPiece, capturedPiece);
    }

    private static Piece CombinePieceMovementAbilities(Piece movingPiece, Piece capturedPiece)
    {
        return new Piece(movingPiece.ColorScheme, movingPiece.Color, movingPiece.PieceType,
            Math.Max(movingPiece.Forward, capturedPiece.Forward),
            Math.Max(movingPiece.ForwardRight, capturedPiece.ForwardRight),
            Math.Max(movingPiece.Right, capturedPiece.Right),
            Math.Max(movingPiece.BackRight, capturedPiece.BackRight),
            Math.Max(movingPiece.Back, capturedPiece.Back),
            Math.Max(movingPiece.BackLeft, capturedPiece.BackLeft),
            Math.Max(movingPiece.Left, capturedPiece.Left),
            Math.Max(movingPiece.ForwardLeft, capturedPiece.ForwardLeft));
    }

    private static Piece Promote(Piece pieceToPromote)
    {
        // Pawns that reach opponent's back rank gain ability to move one square in all directions
        if (pieceToPromote.IsUnpromotedPawn)
        {
            Piece newPiece =
                new Piece(pieceToPromote.ColorScheme, pieceToPromote.Color, pieceToPromote.PieceType,
                    Math.Max(pieceToPromote.Forward, 1),
                    Math.Max(pieceToPromote.ForwardRight, 1),
                    Math.Max(pieceToPromote.Right, 1),
                    Math.Max(pieceToPromote.BackRight, 1),
                    Math.Max(pieceToPromote.Back, 1),
                    Math.Max(pieceToPromote.BackLeft, 1),
                    Math.Max(pieceToPromote.Left, 1),
                    Math.Max(pieceToPromote.ForwardLeft, 1));

            newPiece.IsPromoted = true;

            return newPiece;
        }

        return pieceToPromote;
    }

    internal static bool IsPawnPromotionMove(Piece movingPiece, Square destinationSquare) =>
        movingPiece.IsUnpromotedPawn &&
        ((movingPiece.Color == Color.Light && destinationSquare.Rank == Constants.BackRankDark) ||
         (movingPiece.Color == Color.Dark && destinationSquare.Rank == Constants.BackRankLight));

    #endregion
}