namespace MogriChess.Models.CustomEventArgs;

public class GameEndedEventArgs
{
    public Enums.GameStatus GameEndStatus { get; }

    public GameEndedEventArgs(Enums.GameStatus endStatus)
    {
        GameEndStatus = endStatus;
    }
}