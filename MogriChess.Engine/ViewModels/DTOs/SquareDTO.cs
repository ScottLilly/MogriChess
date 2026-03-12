namespace MogriChess.ViewModels.DTOs
{
    public class SquareDTO
    {
        public int Rank { get; set; }
        public int File { get; set; }
        public PieceDTO Piece { get; set; }
        public bool IsSelected { get; set; }
        public bool IsValidDestination { get; set; }
    }
}