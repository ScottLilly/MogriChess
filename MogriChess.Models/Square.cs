using System.ComponentModel;

namespace MogriChess.Models
{
    public class Square : INotifyPropertyChanged
    {
        public int Rank { get; }
        public int File { get; }
        public string SquareColor { get; }
        public Piece Piece { get; private set; }

        public int UiGridRow => 8 - Rank;
        public int UiGridColumn => File - 1;
        public string FileAsLetter => "ABCDEFGH".Substring(File - 1, 1);

        public event PropertyChangedEventHandler PropertyChanged;

        public Square(int rank, int file, string squareColor)
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