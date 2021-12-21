using MogriChess.Models;

namespace MogriChess.Services
{
    public static class GameFactory
    {
        public static Game GetNewGame()
        {
            return new Game(BoardFactory.GetNewGameBoard());
        }
    }
}