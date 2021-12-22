namespace MogriChess.Models
{
    public class Move
    {
        public Enums.ColorType MovingPieceColor { get; }
        public Square FromSquare { get; }
        public int FromRank => FromSquare.Rank;
        public int FromFile => FromSquare.File;
        public string FromFileAsLetter => FromSquare.FileAsLetter;
        public Square DestinationSquare { get; }
        public int DestinationRank => DestinationSquare.Rank;
        public int DestinationFile => DestinationSquare.File;
        public string DestinationFileAsLetter => DestinationSquare.FileAsLetter;
        public bool IsCapturingMove { get; set; }
        public bool IsWinningMove { get; set; }

        public string MoveShorthand =>
            $"{FromRank}{FromFileAsLetter}:{DestinationRank}{DestinationFileAsLetter}";
        public string MoveResult =>
            IsCapturingMove ? (IsWinningMove ? "Victory" : "Capture") : "";

        public Move(Square fromSquare, Square destinationSquare)
        {
            MovingPieceColor = fromSquare.Piece.ColorType;
            FromSquare = fromSquare;
            DestinationSquare = destinationSquare;
        }
    }
}