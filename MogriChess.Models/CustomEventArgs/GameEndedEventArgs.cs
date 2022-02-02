using System;

namespace MogriChess.Models.CustomEventArgs;

public class GameEndedEventArgs : EventArgs
{
    public Enums.GameStatus GameEndStatus { get; }

    public GameEndedEventArgs(Enums.GameStatus endStatus)
    {
        GameEndStatus = endStatus;
    }
}