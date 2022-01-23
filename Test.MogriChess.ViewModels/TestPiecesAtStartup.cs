using MogriChess.Models;
using MogriChess.Services;
using MogriChess.ViewModels;
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
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(1, FILE_A);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(Constants.UnlimitedMoves, piece.Forward.Squares);
            Assert.Equal(0, piece.ForwardRight.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.Right.Squares);
            Assert.Equal(0, piece.BackRight.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.Back.Squares);
            Assert.Equal(0, piece.BackLeft.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.Left.Squares);
            Assert.Equal(0, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_B1()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(1, FILE_B);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(2, piece.Forward.Squares);
            Assert.Equal(2, piece.ForwardRight.Squares);
            Assert.Equal(2, piece.Right.Squares);
            Assert.Equal(2, piece.BackRight.Squares);
            Assert.Equal(2, piece.Back.Squares);
            Assert.Equal(2, piece.BackLeft.Squares);
            Assert.Equal(2, piece.Left.Squares);
            Assert.Equal(2, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_C1()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(1, FILE_C);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(0, piece.Forward.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.ForwardRight.Squares);
            Assert.Equal(0, piece.Right.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.BackRight.Squares);
            Assert.Equal(0, piece.Back.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.BackLeft.Squares);
            Assert.Equal(0, piece.Left.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_D1()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(1, FILE_D);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(Constants.UnlimitedMoves, piece.Forward.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.ForwardRight.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.Right.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.BackRight.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.Back.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.BackLeft.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.Left.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_E1()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(1, FILE_E);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.True(piece.IsKing);

            Assert.Equal(1, piece.Forward.Squares);
            Assert.Equal(1, piece.ForwardRight.Squares);
            Assert.Equal(1, piece.Right.Squares);
            Assert.Equal(1, piece.BackRight.Squares);
            Assert.Equal(1, piece.Back.Squares);
            Assert.Equal(1, piece.BackLeft.Squares);
            Assert.Equal(1, piece.Left.Squares);
            Assert.Equal(1, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_F1()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(1, FILE_F);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(0, piece.Forward.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.ForwardRight.Squares);
            Assert.Equal(0, piece.Right.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.BackRight.Squares);
            Assert.Equal(0, piece.Back.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.BackLeft.Squares);
            Assert.Equal(0, piece.Left.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_G1()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(1, FILE_G);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(2, piece.Forward.Squares);
            Assert.Equal(2, piece.ForwardRight.Squares);
            Assert.Equal(2, piece.Right.Squares);
            Assert.Equal(2, piece.BackRight.Squares);
            Assert.Equal(2, piece.Back.Squares);
            Assert.Equal(2, piece.BackLeft.Squares);
            Assert.Equal(2, piece.Left.Squares);
            Assert.Equal(2, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_H1()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(1, FILE_H);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(Constants.UnlimitedMoves, piece.Forward.Squares);
            Assert.Equal(0, piece.ForwardRight.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.Right.Squares);
            Assert.Equal(0, piece.BackRight.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.Back.Squares);
            Assert.Equal(0, piece.BackLeft.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.Left.Squares);
            Assert.Equal(0, piece.ForwardLeft.Squares);
        }

        #endregion

        #region Light - Pawns

        [Fact]
        public void Test_ExpectedPiece_A2()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(2, FILE_A);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.Forward.Squares);
            Assert.Equal(1, piece.ForwardRight.Squares);
            Assert.Equal(0, piece.Right.Squares);
            Assert.Equal(0, piece.BackRight.Squares);
            Assert.Equal(0, piece.Back.Squares);
            Assert.Equal(0, piece.BackLeft.Squares);
            Assert.Equal(0, piece.Left.Squares);
            Assert.Equal(1, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_B2()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(2, FILE_B);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.Forward.Squares);
            Assert.Equal(1, piece.ForwardRight.Squares);
            Assert.Equal(0, piece.Right.Squares);
            Assert.Equal(0, piece.BackRight.Squares);
            Assert.Equal(0, piece.Back.Squares);
            Assert.Equal(0, piece.BackLeft.Squares);
            Assert.Equal(0, piece.Left.Squares);
            Assert.Equal(1, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_C2()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(2, FILE_C);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.Forward.Squares);
            Assert.Equal(1, piece.ForwardRight.Squares);
            Assert.Equal(0, piece.Right.Squares);
            Assert.Equal(0, piece.BackRight.Squares);
            Assert.Equal(0, piece.Back.Squares);
            Assert.Equal(0, piece.BackLeft.Squares);
            Assert.Equal(0, piece.Left.Squares);
            Assert.Equal(1, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_D2()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(2, FILE_D);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.Forward.Squares);
            Assert.Equal(1, piece.ForwardRight.Squares);
            Assert.Equal(0, piece.Right.Squares);
            Assert.Equal(0, piece.BackRight.Squares);
            Assert.Equal(0, piece.Back.Squares);
            Assert.Equal(0, piece.BackLeft.Squares);
            Assert.Equal(0, piece.Left.Squares);
            Assert.Equal(1, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_E2()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(2, FILE_E);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.Forward.Squares);
            Assert.Equal(1, piece.ForwardRight.Squares);
            Assert.Equal(0, piece.Right.Squares);
            Assert.Equal(0, piece.BackRight.Squares);
            Assert.Equal(0, piece.Back.Squares);
            Assert.Equal(0, piece.BackLeft.Squares);
            Assert.Equal(0, piece.Left.Squares);
            Assert.Equal(1, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_F2()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(2, FILE_F);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.Forward.Squares);
            Assert.Equal(1, piece.ForwardRight.Squares);
            Assert.Equal(0, piece.Right.Squares);
            Assert.Equal(0, piece.BackRight.Squares);
            Assert.Equal(0, piece.Back.Squares);
            Assert.Equal(0, piece.BackLeft.Squares);
            Assert.Equal(0, piece.Left.Squares);
            Assert.Equal(1, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_G2()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(2, FILE_G);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.Forward.Squares);
            Assert.Equal(1, piece.ForwardRight.Squares);
            Assert.Equal(0, piece.Right.Squares);
            Assert.Equal(0, piece.BackRight.Squares);
            Assert.Equal(0, piece.Back.Squares);
            Assert.Equal(0, piece.BackLeft.Squares);
            Assert.Equal(0, piece.Left.Squares);
            Assert.Equal(1, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_H2()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(2, FILE_H);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Light);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.Forward.Squares);
            Assert.Equal(1, piece.ForwardRight.Squares);
            Assert.Equal(0, piece.Right.Squares);
            Assert.Equal(0, piece.BackRight.Squares);
            Assert.Equal(0, piece.Back.Squares);
            Assert.Equal(0, piece.BackLeft.Squares);
            Assert.Equal(0, piece.Left.Squares);
            Assert.Equal(1, piece.ForwardLeft.Squares);
        }

        #endregion

        #region Empty squares

        [Fact]
        public void Test_ExpectedPieces_Rank_3()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            Assert.Null(game.Board.PieceAt(3, FILE_A));
            Assert.Null(game.Board.PieceAt(3, FILE_B));
            Assert.Null(game.Board.PieceAt(3, FILE_C));
            Assert.Null(game.Board.PieceAt(3, FILE_D));
            Assert.Null(game.Board.PieceAt(3, FILE_E));
            Assert.Null(game.Board.PieceAt(3, FILE_F));
            Assert.Null(game.Board.PieceAt(3, FILE_G));
            Assert.Null(game.Board.PieceAt(3, FILE_H));
        }

        [Fact]
        public void Test_ExpectedPieces_Rank_4()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            Assert.Null(game.Board.PieceAt(4, FILE_A));
            Assert.Null(game.Board.PieceAt(4, FILE_B));
            Assert.Null(game.Board.PieceAt(4, FILE_C));
            Assert.Null(game.Board.PieceAt(4, FILE_D));
            Assert.Null(game.Board.PieceAt(4, FILE_E));
            Assert.Null(game.Board.PieceAt(4, FILE_F));
            Assert.Null(game.Board.PieceAt(4, FILE_G));
            Assert.Null(game.Board.PieceAt(4, FILE_H));
        }

        [Fact]
        public void Test_ExpectedPieces_Rank_5()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            Assert.Null(game.Board.PieceAt(5, FILE_A));
            Assert.Null(game.Board.PieceAt(5, FILE_B));
            Assert.Null(game.Board.PieceAt(5, FILE_C));
            Assert.Null(game.Board.PieceAt(5, FILE_D));
            Assert.Null(game.Board.PieceAt(5, FILE_E));
            Assert.Null(game.Board.PieceAt(5, FILE_F));
            Assert.Null(game.Board.PieceAt(5, FILE_G));
            Assert.Null(game.Board.PieceAt(5, FILE_H));
        }

        [Fact]
        public void Test_ExpectedPieces_Rank_6()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            Assert.Null(game.Board.PieceAt(6, FILE_A));
            Assert.Null(game.Board.PieceAt(6, FILE_B));
            Assert.Null(game.Board.PieceAt(6, FILE_C));
            Assert.Null(game.Board.PieceAt(6, FILE_D));
            Assert.Null(game.Board.PieceAt(6, FILE_E));
            Assert.Null(game.Board.PieceAt(6, FILE_F));
            Assert.Null(game.Board.PieceAt(6, FILE_G));
            Assert.Null(game.Board.PieceAt(6, FILE_H));
        }

        #endregion

        #region Dark - Pawns

        [Fact]
        public void Test_ExpectedPiece_A7()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(7, FILE_A);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.Forward.Squares);
            Assert.Equal(1, piece.ForwardRight.Squares);
            Assert.Equal(0, piece.Right.Squares);
            Assert.Equal(0, piece.BackRight.Squares);
            Assert.Equal(0, piece.Back.Squares);
            Assert.Equal(0, piece.BackLeft.Squares);
            Assert.Equal(0, piece.Left.Squares);
            Assert.Equal(1, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_B7()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(7, FILE_B);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.Forward.Squares);
            Assert.Equal(1, piece.ForwardRight.Squares);
            Assert.Equal(0, piece.Right.Squares);
            Assert.Equal(0, piece.BackRight.Squares);
            Assert.Equal(0, piece.Back.Squares);
            Assert.Equal(0, piece.BackLeft.Squares);
            Assert.Equal(0, piece.Left.Squares);
            Assert.Equal(1, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_C7()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(7, FILE_C);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.Forward.Squares);
            Assert.Equal(1, piece.ForwardRight.Squares);
            Assert.Equal(0, piece.Right.Squares);
            Assert.Equal(0, piece.BackRight.Squares);
            Assert.Equal(0, piece.Back.Squares);
            Assert.Equal(0, piece.BackLeft.Squares);
            Assert.Equal(0, piece.Left.Squares);
            Assert.Equal(1, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_D7()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(7, FILE_D);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.Forward.Squares);
            Assert.Equal(1, piece.ForwardRight.Squares);
            Assert.Equal(0, piece.Right.Squares);
            Assert.Equal(0, piece.BackRight.Squares);
            Assert.Equal(0, piece.Back.Squares);
            Assert.Equal(0, piece.BackLeft.Squares);
            Assert.Equal(0, piece.Left.Squares);
            Assert.Equal(1, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_E7()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(7, FILE_E);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.Forward.Squares);
            Assert.Equal(1, piece.ForwardRight.Squares);
            Assert.Equal(0, piece.Right.Squares);
            Assert.Equal(0, piece.BackRight.Squares);
            Assert.Equal(0, piece.Back.Squares);
            Assert.Equal(0, piece.BackLeft.Squares);
            Assert.Equal(0, piece.Left.Squares);
            Assert.Equal(1, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_F7()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(7, FILE_F);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.Forward.Squares);
            Assert.Equal(1, piece.ForwardRight.Squares);
            Assert.Equal(0, piece.Right.Squares);
            Assert.Equal(0, piece.BackRight.Squares);
            Assert.Equal(0, piece.Back.Squares);
            Assert.Equal(0, piece.BackLeft.Squares);
            Assert.Equal(0, piece.Left.Squares);
            Assert.Equal(1, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_G7()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(7, FILE_G);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.Forward.Squares);
            Assert.Equal(1, piece.ForwardRight.Squares);
            Assert.Equal(0, piece.Right.Squares);
            Assert.Equal(0, piece.BackRight.Squares);
            Assert.Equal(0, piece.Back.Squares);
            Assert.Equal(0, piece.BackLeft.Squares);
            Assert.Equal(0, piece.Left.Squares);
            Assert.Equal(1, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_H7()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(7, FILE_H);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(1, piece.Forward.Squares);
            Assert.Equal(1, piece.ForwardRight.Squares);
            Assert.Equal(0, piece.Right.Squares);
            Assert.Equal(0, piece.BackRight.Squares);
            Assert.Equal(0, piece.Back.Squares);
            Assert.Equal(0, piece.BackLeft.Squares);
            Assert.Equal(0, piece.Left.Squares);
            Assert.Equal(1, piece.ForwardLeft.Squares);
        }

        #endregion

        #region Dark - Major pieces

        [Fact]
        public void Test_ExpectedPiece_A8()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(8, FILE_A);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(Constants.UnlimitedMoves, piece.Forward.Squares);
            Assert.Equal(0, piece.ForwardRight.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.Right.Squares);
            Assert.Equal(0, piece.BackRight.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.Back.Squares);
            Assert.Equal(0, piece.BackLeft.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.Left.Squares);
            Assert.Equal(0, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_B8()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(8, FILE_B);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(2, piece.Forward.Squares);
            Assert.Equal(2, piece.ForwardRight.Squares);
            Assert.Equal(2, piece.Right.Squares);
            Assert.Equal(2, piece.BackRight.Squares);
            Assert.Equal(2, piece.Back.Squares);
            Assert.Equal(2, piece.BackLeft.Squares);
            Assert.Equal(2, piece.Left.Squares);
            Assert.Equal(2, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_C8()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(8, FILE_C);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(0, piece.Forward.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.ForwardRight.Squares);
            Assert.Equal(0, piece.Right.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.BackRight.Squares);
            Assert.Equal(0, piece.Back.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.BackLeft.Squares);
            Assert.Equal(0, piece.Left.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_D8()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(8, FILE_D);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(Constants.UnlimitedMoves, piece.Forward.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.ForwardRight.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.Right.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.BackRight.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.Back.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.BackLeft.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.Left.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_E8()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(8, FILE_E);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.True(piece.IsKing);

            Assert.Equal(1, piece.Forward.Squares);
            Assert.Equal(1, piece.ForwardRight.Squares);
            Assert.Equal(1, piece.Right.Squares);
            Assert.Equal(1, piece.BackRight.Squares);
            Assert.Equal(1, piece.Back.Squares);
            Assert.Equal(1, piece.BackLeft.Squares);
            Assert.Equal(1, piece.Left.Squares);
            Assert.Equal(1, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_F8()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(8, FILE_F);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(0, piece.Forward.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.ForwardRight.Squares);
            Assert.Equal(0, piece.Right.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.BackRight.Squares);
            Assert.Equal(0, piece.Back.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.BackLeft.Squares);
            Assert.Equal(0, piece.Left.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_G8()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(8, FILE_G);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(2, piece.Forward.Squares);
            Assert.Equal(2, piece.ForwardRight.Squares);
            Assert.Equal(2, piece.Right.Squares);
            Assert.Equal(2, piece.BackRight.Squares);
            Assert.Equal(2, piece.Back.Squares);
            Assert.Equal(2, piece.BackLeft.Squares);
            Assert.Equal(2, piece.Left.Squares);
            Assert.Equal(2, piece.ForwardLeft.Squares);
        }

        [Fact]
        public void Test_ExpectedPiece_H8()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            var piece = game.Board.PieceAt(8, FILE_H);

            Assert.NotNull(piece);

            Assert.True(piece.ColorType == Enums.ColorType.Dark);
            Assert.False(piece.IsKing);

            Assert.Equal(Constants.UnlimitedMoves, piece.Forward.Squares);
            Assert.Equal(0, piece.ForwardRight.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.Right.Squares);
            Assert.Equal(0, piece.BackRight.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.Back.Squares);
            Assert.Equal(0, piece.BackLeft.Squares);
            Assert.Equal(Constants.UnlimitedMoves, piece.Left.Squares);
            Assert.Equal(0, piece.ForwardLeft.Squares);
        }

        #endregion
    }
}