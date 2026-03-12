namespace MogriChess.Engine.Models;

/// <summary>
/// Core representation of a move in the engine, independent of UI concerns.
/// </summary>
public class Move
{
    public Move(Square originationSquare, Square destinationSquare)
    {
        OriginationSquare = originationSquare;
        DestinationSquare = destinationSquare;
        MovingPieceColor = originationSquare.Piece.Color;
    }

    public Color MovingPieceColor { get; }
    public Square OriginationSquare { get; }
    public Square DestinationSquare { get; }

    public bool PutsOpponentInCheckmate { get; set; }
    public bool PutsOpponentInCheck { get; set; }
    public bool IsCapturingMove { get; set; }
    public bool IsPromotingMove { get; set; }
    public bool IsDrawFromMaxMoves { get; set; }

    public string MoveShorthand =>
        $"{OriginationSquare.SquareShorthand}:{DestinationSquare.SquareShorthand}";

    public string MoveResult =>
        PutsOpponentInCheckmate ? "Checkmate" :
        PutsOpponentInCheck ? "Check" :
        IsDrawFromMaxMoves ? "Draw" :
        IsCapturingMove ? "Capture" :
        IsPromotingMove ? "Promotion" : "";
}