namespace MogriChess.Engine.Models;

/// <summary>
/// Core representation of a move in the engine, independent of UI concerns.
/// </summary>
public class Move(Square originationSquare, Square destinationSquare)
{
    public Color MovingPieceColor { get; } = originationSquare.Piece.Color;
    public Square OriginationSquare { get; } = originationSquare;
    public Square DestinationSquare { get; } = destinationSquare;

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