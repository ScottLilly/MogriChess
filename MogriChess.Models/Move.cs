namespace MogriChess.Models
{
    public class Move
    {
        public Enums.ColorType MovingPieceColor { get; }
        public Square FromSquare { get; }
        public Square DestinationSquare { get; }

        public int DestinationRank => DestinationSquare.Rank;
        public int DestinationFile => DestinationSquare.File;

        public bool IsCheckmateMove { get; set; }
        public bool IsCheckMove { get; set; }
        public bool IsCapturingMove { get; set; }
        public bool IsPromotingMove { get; set; }

        public string MoveShorthand =>
            $"{FromSquare.SquareShorthand}:{DestinationSquare.SquareShorthand}";

        public string MoveResult =>
            IsCheckmateMove ? "Checkmate" :
            IsCheckMove ? "Check" :
            IsCapturingMove ? "Capture" :
            IsPromotingMove ? "Promotion" : "";

        public Move(Square fromSquare, Square destinationSquare)
        {
            MovingPieceColor = fromSquare.Piece.ColorType;
            FromSquare = fromSquare;
            DestinationSquare = destinationSquare;
        }
    }
}