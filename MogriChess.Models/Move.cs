namespace MogriChess.Models
{
    public class Move
    {
        public Enums.ColorType MovingPieceColor { get; }
        public Square OriginationSquare { get; }
        public Square DestinationSquare { get; }

        public int DestinationRank => DestinationSquare.Rank;
        public int DestinationFile => DestinationSquare.File;

        public bool IsCheckmateMove { get; set; }
        public bool PutsOpponentInCheck { get; set; }
        public bool IsCapturingMove { get; set; }
        public bool IsPromotingMove { get; set; }

        public string MoveShorthand =>
            $"{OriginationSquare.SquareShorthand}:{DestinationSquare.SquareShorthand}";

        public string MoveResult =>
            IsCheckmateMove ? "Checkmate" :
            PutsOpponentInCheck ? "Check" :
            IsCapturingMove ? "Capture" :
            IsPromotingMove ? "Promotion" : "";

        public Move(Square originationSquare, Square destinationSquare)
        {
            MovingPieceColor = originationSquare.Piece.ColorType;
            OriginationSquare = originationSquare;
            DestinationSquare = destinationSquare;
        }
    }
}