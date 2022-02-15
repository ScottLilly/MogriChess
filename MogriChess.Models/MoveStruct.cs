namespace MogriChess.Models
{
    public record struct MoveStruct
    {
        public string MovingPieceColor { get; set; }
        public string MoveShorthand { get; set; }
        public string MoveResult { get; set; }
    }
}