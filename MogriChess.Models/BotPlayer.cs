using System.Collections.Generic;
using System.Linq;

namespace MogriChess.Models
{
    public class BotPlayer
    {
        public Enums.ColorType ColorType { get; }

        public BotPlayer(Enums.ColorType colorType)
        {
            ColorType = colorType;
        }

        public Move FindBestMove(List<Move> potentialMoves)
        {
            // TODO: Make this much more intelligent
            return potentialMoves.First();
        }
    }
}