using MogriChess.Engine.Models;
using System;

namespace MogriChess.Engine.CustomEventArgs;

public class GameEndedEventArgs(GameStatus endStatus) : EventArgs
{
    public GameStatus GameEndStatus { get; } = endStatus;
}