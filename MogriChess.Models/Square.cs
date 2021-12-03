using System.ComponentModel;

namespace MogriChess.Models
{
    public class Square : INotifyPropertyChanged
    {
        public int Rank { get; }
        public int File { get; }
        public ColorScheme ColorScheme { get; }
        public Enums.ColorType ColorType { get; }
        public Piece Piece { get; private set; }

        public string SquareColor =>
            ColorType == Enums.ColorType.Light ? ColorScheme.LightColor : ColorScheme.DarkColor;

        public int UiGridRow => 8 - Rank;
        public int UiGridColumn => File - 1;
        public string FileAsLetter => "ABCDEFGH".Substring(File - 1, 1);

        public event PropertyChangedEventHandler PropertyChanged;

        public Square(int rank, int file, ColorScheme colorScheme, Enums.ColorType color)
        {
            Rank = rank;
            File = file;
            ColorScheme = colorScheme;
            ColorType = color;
        }

        public void PlacePiece(Piece piece)
        {
            Piece = piece;

            // TODO: handle captures
        }
    }
}