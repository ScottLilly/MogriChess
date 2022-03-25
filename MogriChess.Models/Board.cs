using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MogriChess.Core;

namespace MogriChess.Models;

public class Board : INotifyPropertyChanged
{
    public ColorScheme BoardColorScheme { get; set; }
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

    public IEnumerable<Move> LegalMovesForPieceAt(int rank, int file) =>
        PotentialMovesForPieceAt(ModelFunctions.GetShorthand(rank, file))
            .Where(m => GetSimulatedMoveResult(m, () => KingCannotBeCaptured(m.MovingPieceColor)));

    public void MovePiece(Square originationSquare, Square destinationSquare)
    {
        PlacePieceOnSquare(originationSquare.Piece, destinationSquare);
        originationSquare.Piece = null;
    }

    public bool KingCanBeCaptured(Enums.Color playerColor) =>
        SquaresWithPiecesOfColor(playerColor.OppositeColor())
            .Any(square => PotentialMovesForPieceAt(square)
                .Any(m => m.PutsOpponentInCheck));

    public void ClearValidDestinations() =>
        Squares.Values.ApplyToEach(s => s.IsValidDestination = false);

    public IEnumerable<Square> SquaresWithPiecesOfColor(Enums.Color color) =>
        Squares.Values.Where(s => s.Piece?.Color == color);

    public List<Move> LegalMovesForPlayer(Enums.Color playerColor) =>
        SquaresWithPiecesOfColor(playerColor)
            .SelectMany(square => LegalMovesForPieceAt(square.Rank, square.File))
            .ToList();

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

    public List<Move> PotentialMovesForPieceAt(Square square) =>
        PotentialMovesForPieceAt(square.SquareShorthand);

    public bool KingCannotBeCaptured(Enums.Color playerColor) =>
        !KingCanBeCaptured(playerColor);

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
            }
        }
    }

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
            MaxMovementSquaresForDirection(movingPiece, direction);
        (int rankMultiplier, int fileMultiplier) =
            MovementMultipliersForDirection(movingPiece, direction);

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

    private int MaxMovementSquaresForDirection(Piece piece, Enums.Direction direction)
    {
        return direction switch
        {
            Enums.Direction.Forward => piece.Forward,
            Enums.Direction.ForwardRight => piece.ForwardRight,
            Enums.Direction.Right => piece.Right,
            Enums.Direction.BackRight => piece.BackRight,
            Enums.Direction.Back => piece.Back,
            Enums.Direction.BackLeft => piece.BackLeft,
            Enums.Direction.Left => piece.Left,
            Enums.Direction.ForwardLeft => piece.ForwardLeft,
            _ => throw new InvalidEnumArgumentException(
                "Invalid enum passed to MaxMovementSquaresForDirection() function")
        };
    }

    private (int rankMultiplier, int fileMultiplier) MovementMultipliersForDirection(Piece piece, Enums.Direction direction)
    {
        (int rm, int fm) multipliers = direction switch
        {
            Enums.Direction.Forward => (1, 0),
            Enums.Direction.ForwardRight => (1, 1),
            Enums.Direction.Right => (0, 1),
            Enums.Direction.BackRight => (-1, 1),
            Enums.Direction.Back => (-1, 0),
            Enums.Direction.BackLeft => (-1, -1),
            Enums.Direction.Left => (0, -1),
            Enums.Direction.ForwardLeft => (1, -1),
            _ => throw new InvalidEnumArgumentException(
                "Invalid direction parameter sent to MovementMultipliersForDirection")
        };

        return piece.Color == Enums.Color.Light
            ? multipliers
            : (-multipliers.rm, -multipliers.fm);
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

    private static bool IsPawnPromotionMove(Piece movingPiece, Square destinationSquare) =>
        movingPiece.IsUnpromotedPawn &&
        ((movingPiece.Color == Enums.Color.Light && destinationSquare.Rank == Constants.BackRankDark) ||
         (movingPiece.Color == Enums.Color.Dark && destinationSquare.Rank == Constants.BackRankLight));

    #endregion
}