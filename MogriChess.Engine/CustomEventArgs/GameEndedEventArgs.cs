using MogriChess.Engine.Models;
using System;

namespace MogriChess.Engine.CustomEventArgs;

public class GameEndedEventArgs(Enums.GameStatus endStatus) : EventArgs
{
    public Enums.GameStatus GameEndStatus { get; } = endStatus;
}