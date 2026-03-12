using System;

namespace MogriChess.Models.CustomEventArgs;

public class GameEndedEventArgs(Enums.GameStatus endStatus) : EventArgs
{
    public Enums.GameStatus GameEndStatus { get; } = endStatus;
}