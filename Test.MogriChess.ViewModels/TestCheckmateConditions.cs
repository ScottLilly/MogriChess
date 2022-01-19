using System.Collections.Generic;
using System.Linq;
using MogriChess.Models;
using MogriChess.Services;
using Test.MogriChess.ViewModels.Utilities;
using Xunit;

namespace Test.MogriChess.ViewModels
{
    public class TestCheckmateConditions
    {
        [Fact]
        public void Test_Game1()
        {
            Game game = GameFactory.GetNewGame();

            List<string> moveNotations =
                MoveHistoryParser.GetMovesFromFile(".\\MoveHistories\\BadCheck1_MoveHistory.json");

            foreach (string moveNotation in moveNotations)
            {
                var squares = moveNotation.Split(":");

                Square originationSquare =
                    game.Board.Squares.First(s => s.SquareShorthand.Equals(squares[0]));
                Square destinationSquare =
                    game.Board.Squares.First(s => s.SquareShorthand.Equals(squares[1]));

                game.SelectSquare(originationSquare);
                game.SelectSquare(destinationSquare);
            }

            Assert.False(game.MoveHistory.Last().IsCheckmateMove);
        }
    }
}