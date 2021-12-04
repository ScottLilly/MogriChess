using MogriChess.ViewModels;
using MogriChess.Models;

using Xunit;

namespace Test.MogriChess.ViewModels
{
    public class TestPiecesAtStartup
    {
        [Fact]
        public void Test_ExpectedPiece_A2()
        {
            Game game = new Game();

            var a2Pawn = game.PieceAt(2, 1);

            Assert.NotNull(a2Pawn);

            Assert.True(a2Pawn.ColorType == Enums.ColorType.Light);
            Assert.False(a2Pawn.IsKing);

            Assert.Equal(1, a2Pawn.SquaresForward);
            Assert.Equal(1, a2Pawn.SquaresForwardRight);
            Assert.Equal(0, a2Pawn.SquaresRight);
            Assert.Equal(0, a2Pawn.SquaresBackRight);
            Assert.Equal(0, a2Pawn.SquaresBack);
            Assert.Equal(0, a2Pawn.SquaresBackLeft);
            Assert.Equal(0, a2Pawn.SquaresLeft);
            Assert.Equal(1, a2Pawn.SquaresForwardLeft);
        }
    }
}
