using System.ComponentModel;
using System.Linq;
using MogriChess.Models;
using MogriChess.Services;

namespace MogriChess.ViewModels
{
    public class Game : INotifyPropertyChanged
    {
        public const string PIECE_COLOR_DARK = "#000000";
        public const string PIECE_COLOR_LIGHT = "#FFFFFF";
        
        public event PropertyChangedEventHandler PropertyChanged;

        public Board Board { get; }

        public Game()
        {
            Board = new Board();

            PopulatePieces(PIECE_COLOR_LIGHT);
            PopulatePieces(PIECE_COLOR_DARK);
        }

        private void PopulatePieces(string color)
        {
            int pawnRow = color.Equals(PIECE_COLOR_LIGHT) ? 2 : 7;
            int majorPieceRow = color.Equals(PIECE_COLOR_LIGHT) ? 1 : 8;
            
            Enums.PlayerColor colorEnum = 
                color.Equals(PIECE_COLOR_LIGHT) ? Enums.PlayerColor.Light : Enums.PlayerColor.Dark;

            // Add major pieces
            GetSquareAt(majorPieceRow, 1).PlacePiece(PieceFactory.GetRook(colorEnum));
            GetSquareAt(majorPieceRow, 2).PlacePiece(PieceFactory.GetKnight(colorEnum));
            GetSquareAt(majorPieceRow, 3).PlacePiece(PieceFactory.GetBishop(colorEnum));
            GetSquareAt(majorPieceRow, 4).PlacePiece(PieceFactory.GetQueen(colorEnum));
            GetSquareAt(majorPieceRow, 5).PlacePiece(PieceFactory.GetKing(colorEnum));
            GetSquareAt(majorPieceRow, 6).PlacePiece(PieceFactory.GetBishop(colorEnum));
            GetSquareAt(majorPieceRow, 7).PlacePiece(PieceFactory.GetKnight(colorEnum));
            GetSquareAt(majorPieceRow, 8).PlacePiece(PieceFactory.GetRook(colorEnum));

            // Add pawns
            GetSquareAt(pawnRow, 1).PlacePiece(PieceFactory.GetPawn(colorEnum));
            GetSquareAt(pawnRow, 2).PlacePiece(PieceFactory.GetPawn(colorEnum));
            GetSquareAt(pawnRow, 3).PlacePiece(PieceFactory.GetPawn(colorEnum));
            GetSquareAt(pawnRow, 4).PlacePiece(PieceFactory.GetPawn(colorEnum));
            GetSquareAt(pawnRow, 5).PlacePiece(PieceFactory.GetPawn(colorEnum));
            GetSquareAt(pawnRow, 6).PlacePiece(PieceFactory.GetPawn(colorEnum));
            GetSquareAt(pawnRow, 7).PlacePiece(PieceFactory.GetPawn(colorEnum));
            GetSquareAt(pawnRow, 8).PlacePiece(PieceFactory.GetPawn(colorEnum));
        }

        private Square GetSquareAt(int rank, int file)
        {
            return Board.Squares.First(s => s.Rank.Equals(rank) && s.File.Equals(file));
        }
    }
}