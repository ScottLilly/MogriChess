using System.Collections.Generic;

namespace MogriChess.Models;

public class GameState
{
    public Enums.Color CurrentPlayerColor { get; }

    public List<Square> Squares { get; } =
        new List<Square>();

    public GameState(Game currentGame)
    {
        CurrentPlayerColor = currentGame.CurrentPlayerColor;
        Squares.AddRange(currentGame.Board.Squares);
    }
}