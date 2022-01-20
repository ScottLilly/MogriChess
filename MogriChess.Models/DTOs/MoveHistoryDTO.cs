using Newtonsoft.Json;

namespace MogriChess.Models.DTOs
{
    public class MoveHistoryDTO
    {
        public string MoveShorthand { get; set; }
        public bool IsCheckmateMove { get; set; }
        public bool PutsOpponentInCheck { get; set; }
        public bool IsCapturingMove { get; set; }
        public bool IsPromotingMove { get; set; }

        [JsonIgnore]
        public string OriginationSquare => MoveShorthand.Substring(0, 2);
        [JsonIgnore]
        public string DestinationSquare => MoveShorthand.Substring(3, 2);
    }
}