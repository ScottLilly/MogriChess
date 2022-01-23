using System.Collections.Generic;
using System.Linq;
using MogriChess.Models;
using MogriChess.Models.DTOs;
using MogriChess.Services;
using MogriChess.ViewModels;
using Test.MogriChess.ViewModels.Utilities;
using Xunit;

namespace Test.MogriChess.ViewModels
{
    public class TestCheckmateConditions
    {
        [Fact]
        public void Test_IncorrectCheckmateReported_1()
        {
            // Test fix for a reported false checkmate
            // https://github.com/ScottLilly/MogriChess/issues/3

            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            List<MoveHistoryDTO> moves =
                TestFileParser.GetMoveHistoryFromFile(".\\MoveHistories\\BadCheckmate_1_MoveHistory.json");

            foreach (MoveHistoryDTO move in moves)
            {
                Square originationSquare =
                    game.Board.Squares.First(s => s.SquareShorthand.Equals(move.OriginationSquare));
                Square destinationSquare =
                    game.Board.Squares.First(s => s.SquareShorthand.Equals(move.DestinationSquare));

                game.SelectSquare(originationSquare);
                game.SelectSquare(destinationSquare);
            }

            Assert.False(game.MoveHistory.Last().IsCheckmateMove);
        }
    }
}