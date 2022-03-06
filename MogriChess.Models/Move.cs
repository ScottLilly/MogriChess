using System.ComponentModel;

namespace MogriChess.Models;

public class Move : INotifyPropertyChanged
{
    public Enums.Color MovingPieceColor { get; }
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

    public event PropertyChangedEventHandler PropertyChanged;

    public Move(Square originationSquare, Square destinationSquare)
    {
        MovingPieceColor = originationSquare.Piece.Color;
        OriginationSquare = originationSquare;
        DestinationSquare = destinationSquare;
    }
}