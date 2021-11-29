using System.ComponentModel;
using System.Drawing;

namespace MogriChess.Models
{
    public class Square : INotifyPropertyChanged
    {
        public int Rank { get; }
        public int File { get; }
        public Color SquareColor { get; }
        public Piece Piece { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Square(int rank, int file, Color squareColor)
        {
            Rank = rank;
            File = file;
            SquareColor = squareColor;
        }

        public void PlacePiece(Piece piece)
        {
            Piece = piece;

            // TODO: handle captures
        }
    }
}