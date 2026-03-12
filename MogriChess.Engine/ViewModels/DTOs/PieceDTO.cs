namespace MogriChess.ViewModels.DTOs
{
    public class PieceDTO
    {
        public string Color { get; set; }

        public int ForwardSquares { get; set; }
        public int ForwardRightSquares { get; set; }
        public int RightSquares { get; set; }
        public int BackRightSquares { get; set; }
        public int BackSquares { get; set; }
        public int BackLeftSquares { get; set; }
        public int LeftSquares { get; set; }
        public int ForwardLeftSquares { get; set; }

        public bool IsKing { get; set; }
        public bool IsUnpromotedPawn { get; set; }
    }
}