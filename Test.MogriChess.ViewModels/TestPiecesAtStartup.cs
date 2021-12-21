using MogriChess.Models;
using MogriChess.Services;
using Xunit;

namespace Test.MogriChess.ViewModels
{
    public class TestPiecesAtStartup
    {
        private const int FILE_A = 1;
        private const int FILE_B = 2;
        private const int FILE_C = 3;
        private const int FILE_D = 4;
        private const int FILE_E = 5;
        private const int FILE_F = 6;
        private const int FILE_G = 7;
        private const int FILE_H = 8;

        #region Light - Major pieces

        [Fact]
        public void Test_ExpectedPiece_A1()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(1, FILE_A);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(99, piece.SquaresForward);
            Assert.Equal(0, piece.SquaresForwardRight);
            Assert.Equal(99, piece.SquaresRight);
            Assert.Equal(0, piece.SquaresBackRight);
            Assert.Equal(99, piece.SquaresBack);
            Assert.Equal(0, piece.SquaresBackLeft);
            Assert.Equal(99, piece.SquaresLeft);
            Assert.Equal(0, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_B1()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(1, FILE_B);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(2, piece.SquaresForward);
            Assert.Equal(2, piece.SquaresForwardRight);
            Assert.Equal(2, piece.SquaresRight);
            Assert.Equal(2, piece.SquaresBackRight);
            Assert.Equal(2, piece.SquaresBack);
            Assert.Equal(2, piece.SquaresBackLeft);
            Assert.Equal(2, piece.SquaresLeft);
            Assert.Equal(2, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_C1()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(1, FILE_C);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(0, piece.SquaresForward);
            Assert.Equal(99, piece.SquaresForwardRight);
            Assert.Equal(0, piece.SquaresRight);
            Assert.Equal(99, piece.SquaresBackRight);
            Assert.Equal(0, piece.SquaresBack);
            Assert.Equal(99, piece.SquaresBackLeft);
            Assert.Equal(0, piece.SquaresLeft);
            Assert.Equal(99, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_D1()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(1, FILE_D);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(99, piece.SquaresForward);
            Assert.Equal(99, piece.SquaresForwardRight);
            Assert.Equal(99, piece.SquaresRight);
            Assert.Equal(99, piece.SquaresBackRight);
            Assert.Equal(99, piece.SquaresBack);
            Assert.Equal(99, piece.SquaresBackLeft);
            Assert.Equal(99, piece.SquaresLeft);
            Assert.Equal(99, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_E1()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(1, FILE_E);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.True(piece.IsKing);

            Assert.Equal(1, piece.SquaresForward);
            Assert.Equal(1, piece.SquaresForwardRight);
            Assert.Equal(1, piece.SquaresRight);
            Assert.Equal(1, piece.SquaresBackRight);
            Assert.Equal(1, piece.SquaresBack);
            Assert.Equal(1, piece.SquaresBackLeft);
            Assert.Equal(1, piece.SquaresLeft);
            Assert.Equal(1, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_F1()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(1, FILE_F);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(0, piece.SquaresForward);
            Assert.Equal(99, piece.SquaresForwardRight);
            Assert.Equal(0, piece.SquaresRight);
            Assert.Equal(99, piece.SquaresBackRight);
            Assert.Equal(0, piece.SquaresBack);
            Assert.Equal(99, piece.SquaresBackLeft);
            Assert.Equal(0, piece.SquaresLeft);
            Assert.Equal(99, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_G1()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(1, FILE_G);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(2, piece.SquaresForward);
            Assert.Equal(2, piece.SquaresForwardRight);
            Assert.Equal(2, piece.SquaresRight);
            Assert.Equal(2, piece.SquaresBackRight);
            Assert.Equal(2, piece.SquaresBack);
            Assert.Equal(2, piece.SquaresBackLeft);
            Assert.Equal(2, piece.SquaresLeft);
            Assert.Equal(2, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_H1()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(1, FILE_H);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(99, piece.SquaresForward);
            Assert.Equal(0, piece.SquaresForwardRight);
            Assert.Equal(99, piece.SquaresRight);
            Assert.Equal(0, piece.SquaresBackRight);
            Assert.Equal(99, piece.SquaresBack);
            Assert.Equal(0, piece.SquaresBackLeft);
            Assert.Equal(99, piece.SquaresLeft);
            Assert.Equal(0, piece.SquaresForwardLeft);
        }

        #endregion

        #region Light - Pawns

        [Fact]
        public void Test_ExpectedPiece_A2()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(2, FILE_A);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.SquaresForward);
            Assert.Equal(1, piece.SquaresForwardRight);
            Assert.Equal(0, piece.SquaresRight);
            Assert.Equal(0, piece.SquaresBackRight);
            Assert.Equal(0, piece.SquaresBack);
            Assert.Equal(0, piece.SquaresBackLeft);
            Assert.Equal(0, piece.SquaresLeft);
            Assert.Equal(1, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_B2()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(2, FILE_B);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.SquaresForward);
            Assert.Equal(1, piece.SquaresForwardRight);
            Assert.Equal(0, piece.SquaresRight);
            Assert.Equal(0, piece.SquaresBackRight);
            Assert.Equal(0, piece.SquaresBack);
            Assert.Equal(0, piece.SquaresBackLeft);
            Assert.Equal(0, piece.SquaresLeft);
            Assert.Equal(1, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_C2()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(2, FILE_C);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.SquaresForward);
            Assert.Equal(1, piece.SquaresForwardRight);
            Assert.Equal(0, piece.SquaresRight);
            Assert.Equal(0, piece.SquaresBackRight);
            Assert.Equal(0, piece.SquaresBack);
            Assert.Equal(0, piece.SquaresBackLeft);
            Assert.Equal(0, piece.SquaresLeft);
            Assert.Equal(1, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_D2()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(2, FILE_D);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.SquaresForward);
            Assert.Equal(1, piece.SquaresForwardRight);
            Assert.Equal(0, piece.SquaresRight);
            Assert.Equal(0, piece.SquaresBackRight);
            Assert.Equal(0, piece.SquaresBack);
            Assert.Equal(0, piece.SquaresBackLeft);
            Assert.Equal(0, piece.SquaresLeft);
            Assert.Equal(1, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_E2()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(2, FILE_E);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.SquaresForward);
            Assert.Equal(1, piece.SquaresForwardRight);
            Assert.Equal(0, piece.SquaresRight);
            Assert.Equal(0, piece.SquaresBackRight);
            Assert.Equal(0, piece.SquaresBack);
            Assert.Equal(0, piece.SquaresBackLeft);
            Assert.Equal(0, piece.SquaresLeft);
            Assert.Equal(1, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_F2()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(2, FILE_F);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.SquaresForward);
            Assert.Equal(1, piece.SquaresForwardRight);
            Assert.Equal(0, piece.SquaresRight);
            Assert.Equal(0, piece.SquaresBackRight);
            Assert.Equal(0, piece.SquaresBack);
            Assert.Equal(0, piece.SquaresBackLeft);
            Assert.Equal(0, piece.SquaresLeft);
            Assert.Equal(1, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_G2()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(2, FILE_G);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.SquaresForward);
            Assert.Equal(1, piece.SquaresForwardRight);
            Assert.Equal(0, piece.SquaresRight);
            Assert.Equal(0, piece.SquaresBackRight);
            Assert.Equal(0, piece.SquaresBack);
            Assert.Equal(0, piece.SquaresBackLeft);
            Assert.Equal(0, piece.SquaresLeft);
            Assert.Equal(1, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_H2()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(2, FILE_H);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.SquaresForward);
            Assert.Equal(1, piece.SquaresForwardRight);
            Assert.Equal(0, piece.SquaresRight);
            Assert.Equal(0, piece.SquaresBackRight);
            Assert.Equal(0, piece.SquaresBack);
            Assert.Equal(0, piece.SquaresBackLeft);
            Assert.Equal(0, piece.SquaresLeft);
            Assert.Equal(1, piece.SquaresForwardLeft);
        }

        #endregion

        #region Empty squares

        [Fact]
        public void Test_ExpectedPieces_Rank_3()
        {
            Game game = GameFactory.GetNewGame();

            Assert.Null(game.PieceAt(3, FILE_A));
            Assert.Null(game.PieceAt(3, FILE_B));
            Assert.Null(game.PieceAt(3, FILE_C));
            Assert.Null(game.PieceAt(3, FILE_D));
            Assert.Null(game.PieceAt(3, FILE_E));
            Assert.Null(game.PieceAt(3, FILE_F));
            Assert.Null(game.PieceAt(3, FILE_G));
            Assert.Null(game.PieceAt(3, FILE_H));
        }

        [Fact]
        public void Test_ExpectedPieces_Rank_4()
        {
            Game game = GameFactory.GetNewGame();

            Assert.Null(game.PieceAt(4, FILE_A));
            Assert.Null(game.PieceAt(4, FILE_B));
            Assert.Null(game.PieceAt(4, FILE_C));
            Assert.Null(game.PieceAt(4, FILE_D));
            Assert.Null(game.PieceAt(4, FILE_E));
            Assert.Null(game.PieceAt(4, FILE_F));
            Assert.Null(game.PieceAt(4, FILE_G));
            Assert.Null(game.PieceAt(4, FILE_H));
        }

        [Fact]
        public void Test_ExpectedPieces_Rank_5()
        {
            Game game = GameFactory.GetNewGame();

            Assert.Null(game.PieceAt(5, FILE_A));
            Assert.Null(game.PieceAt(5, FILE_B));
            Assert.Null(game.PieceAt(5, FILE_C));
            Assert.Null(game.PieceAt(5, FILE_D));
            Assert.Null(game.PieceAt(5, FILE_E));
            Assert.Null(game.PieceAt(5, FILE_F));
            Assert.Null(game.PieceAt(5, FILE_G));
            Assert.Null(game.PieceAt(5, FILE_H));
        }

        [Fact]
        public void Test_ExpectedPieces_Rank_6()
        {
            Game game = GameFactory.GetNewGame();

            Assert.Null(game.PieceAt(6, FILE_A));
            Assert.Null(game.PieceAt(6, FILE_B));
            Assert.Null(game.PieceAt(6, FILE_C));
            Assert.Null(game.PieceAt(6, FILE_D));
            Assert.Null(game.PieceAt(6, FILE_E));
            Assert.Null(game.PieceAt(6, FILE_F));
            Assert.Null(game.PieceAt(6, FILE_G));
            Assert.Null(game.PieceAt(6, FILE_H));
        }

        #endregion

        #region Dark - Pawns

        [Fact]
        public void Test_ExpectedPiece_A7()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(7, FILE_A);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.SquaresForward);
            Assert.Equal(1, piece.SquaresForwardRight);
            Assert.Equal(0, piece.SquaresRight);
            Assert.Equal(0, piece.SquaresBackRight);
            Assert.Equal(0, piece.SquaresBack);
            Assert.Equal(0, piece.SquaresBackLeft);
            Assert.Equal(0, piece.SquaresLeft);
            Assert.Equal(1, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_B7()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(7, FILE_B);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.SquaresForward);
            Assert.Equal(1, piece.SquaresForwardRight);
            Assert.Equal(0, piece.SquaresRight);
            Assert.Equal(0, piece.SquaresBackRight);
            Assert.Equal(0, piece.SquaresBack);
            Assert.Equal(0, piece.SquaresBackLeft);
            Assert.Equal(0, piece.SquaresLeft);
            Assert.Equal(1, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_C7()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(7, FILE_C);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.SquaresForward);
            Assert.Equal(1, piece.SquaresForwardRight);
            Assert.Equal(0, piece.SquaresRight);
            Assert.Equal(0, piece.SquaresBackRight);
            Assert.Equal(0, piece.SquaresBack);
            Assert.Equal(0, piece.SquaresBackLeft);
            Assert.Equal(0, piece.SquaresLeft);
            Assert.Equal(1, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_D7()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(7, FILE_D);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.SquaresForward);
            Assert.Equal(1, piece.SquaresForwardRight);
            Assert.Equal(0, piece.SquaresRight);
            Assert.Equal(0, piece.SquaresBackRight);
            Assert.Equal(0, piece.SquaresBack);
            Assert.Equal(0, piece.SquaresBackLeft);
            Assert.Equal(0, piece.SquaresLeft);
            Assert.Equal(1, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_E7()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(7, FILE_E);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.SquaresForward);
            Assert.Equal(1, piece.SquaresForwardRight);
            Assert.Equal(0, piece.SquaresRight);
            Assert.Equal(0, piece.SquaresBackRight);
            Assert.Equal(0, piece.SquaresBack);
            Assert.Equal(0, piece.SquaresBackLeft);
            Assert.Equal(0, piece.SquaresLeft);
            Assert.Equal(1, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_F7()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(7, FILE_F);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.SquaresForward);
            Assert.Equal(1, piece.SquaresForwardRight);
            Assert.Equal(0, piece.SquaresRight);
            Assert.Equal(0, piece.SquaresBackRight);
            Assert.Equal(0, piece.SquaresBack);
            Assert.Equal(0, piece.SquaresBackLeft);
            Assert.Equal(0, piece.SquaresLeft);
            Assert.Equal(1, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_G7()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(7, FILE_G);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.SquaresForward);
            Assert.Equal(1, piece.SquaresForwardRight);
            Assert.Equal(0, piece.SquaresRight);
            Assert.Equal(0, piece.SquaresBackRight);
            Assert.Equal(0, piece.SquaresBack);
            Assert.Equal(0, piece.SquaresBackLeft);
            Assert.Equal(0, piece.SquaresLeft);
            Assert.Equal(1, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_H7()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(7, FILE_H);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.SquaresForward);
            Assert.Equal(1, piece.SquaresForwardRight);
            Assert.Equal(0, piece.SquaresRight);
            Assert.Equal(0, piece.SquaresBackRight);
            Assert.Equal(0, piece.SquaresBack);
            Assert.Equal(0, piece.SquaresBackLeft);
            Assert.Equal(0, piece.SquaresLeft);
            Assert.Equal(1, piece.SquaresForwardLeft);
        }

        #endregion

        #region Dark - Major pieces

        [Fact]
        public void Test_ExpectedPiece_A8()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(8, FILE_A);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(99, piece.SquaresForward);
            Assert.Equal(0, piece.SquaresForwardRight);
            Assert.Equal(99, piece.SquaresRight);
            Assert.Equal(0, piece.SquaresBackRight);
            Assert.Equal(99, piece.SquaresBack);
            Assert.Equal(0, piece.SquaresBackLeft);
            Assert.Equal(99, piece.SquaresLeft);
            Assert.Equal(0, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_B8()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(8, FILE_B);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(2, piece.SquaresForward);
            Assert.Equal(2, piece.SquaresForwardRight);
            Assert.Equal(2, piece.SquaresRight);
            Assert.Equal(2, piece.SquaresBackRight);
            Assert.Equal(2, piece.SquaresBack);
            Assert.Equal(2, piece.SquaresBackLeft);
            Assert.Equal(2, piece.SquaresLeft);
            Assert.Equal(2, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_C8()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(8, FILE_C);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(0, piece.SquaresForward);
            Assert.Equal(99, piece.SquaresForwardRight);
            Assert.Equal(0, piece.SquaresRight);
            Assert.Equal(99, piece.SquaresBackRight);
            Assert.Equal(0, piece.SquaresBack);
            Assert.Equal(99, piece.SquaresBackLeft);
            Assert.Equal(0, piece.SquaresLeft);
            Assert.Equal(99, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_D8()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(8, FILE_D);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(99, piece.SquaresForward);
            Assert.Equal(99, piece.SquaresForwardRight);
            Assert.Equal(99, piece.SquaresRight);
            Assert.Equal(99, piece.SquaresBackRight);
            Assert.Equal(99, piece.SquaresBack);
            Assert.Equal(99, piece.SquaresBackLeft);
            Assert.Equal(99, piece.SquaresLeft);
            Assert.Equal(99, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_E8()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(8, FILE_E);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.True(piece.IsKing);

            Assert.Equal(1, piece.SquaresForward);
            Assert.Equal(1, piece.SquaresForwardRight);
            Assert.Equal(1, piece.SquaresRight);
            Assert.Equal(1, piece.SquaresBackRight);
            Assert.Equal(1, piece.SquaresBack);
            Assert.Equal(1, piece.SquaresBackLeft);
            Assert.Equal(1, piece.SquaresLeft);
            Assert.Equal(1, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_F8()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(8, FILE_F);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(0, piece.SquaresForward);
            Assert.Equal(99, piece.SquaresForwardRight);
            Assert.Equal(0, piece.SquaresRight);
            Assert.Equal(99, piece.SquaresBackRight);
            Assert.Equal(0, piece.SquaresBack);
            Assert.Equal(99, piece.SquaresBackLeft);
            Assert.Equal(0, piece.SquaresLeft);
            Assert.Equal(99, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_G8()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(8, FILE_G);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(2, piece.SquaresForward);
            Assert.Equal(2, piece.SquaresForwardRight);
            Assert.Equal(2, piece.SquaresRight);
            Assert.Equal(2, piece.SquaresBackRight);
            Assert.Equal(2, piece.SquaresBack);
            Assert.Equal(2, piece.SquaresBackLeft);
            Assert.Equal(2, piece.SquaresLeft);
            Assert.Equal(2, piece.SquaresForwardLeft);
        }

        [Fact]
        public void Test_ExpectedPiece_H8()
        {
            Game game = GameFactory.GetNewGame();

            var piece = game.PieceAt(8, FILE_H);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(99, piece.SquaresForward);
            Assert.Equal(0, piece.SquaresForwardRight);
            Assert.Equal(99, piece.SquaresRight);
            Assert.Equal(0, piece.SquaresBackRight);
            Assert.Equal(99, piece.SquaresBack);
            Assert.Equal(0, piece.SquaresBackLeft);
            Assert.Equal(99, piece.SquaresLeft);
            Assert.Equal(0, piece.SquaresForwardLeft);
        }

        #endregion
    }
}