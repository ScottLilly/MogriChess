namespace MogriChess.Models.CustomEventArgs
{
    public class GameEndedEventArgs
    {
        public enum EndCondition
        {
            LightWonByCheckmate,
            DarkWonByCheckmate
        }

        public EndCondition GameEndCondition { get; }

        public GameEndedEventArgs(EndCondition endCondition)
        {
            GameEndCondition = endCondition;
        }
    }
}