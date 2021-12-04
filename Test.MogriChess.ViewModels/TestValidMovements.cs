using MogriChess.ViewModels;

using Xunit;

namespace Test.MogriChess.ViewModels
{
    public class TestValidMovements
    {
        [Fact]
        public void Test_ValidMovesForPiece_A2()
        {
            Game game = new Game();

            var validDestinations = game.ValidDestinationsForPieceAt(2, 1);

            Assert.Equal(2, validDestinations.Count);
            Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(1)));
            Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(2)));
        }

        [Fact]
        public void Test_ValidMovesForPiece_B2()
        {
            Game game = new Game();

            var validDestinations = game.ValidDestinationsForPieceAt(2, 2);

            Assert.Equal(3, validDestinations.Count);
            Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(1)));
            Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(2)));
            Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(3)));
        }
    }
}