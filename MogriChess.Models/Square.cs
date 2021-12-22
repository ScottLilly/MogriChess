using System.ComponentModel;

namespace MogriChess.Models
{
    public class Square : INotifyPropertyChanged
    {
        private ColorScheme _colorScheme;

        public int Rank { get; }
        public int File { get; }
        public Enums.ColorType ColorType { get; }
        public Piece Piece { get; set; }
        public bool IsSelected { get; set; }

        public string SquareColor =>
            IsSelected
                ? "#3399FF"
                : ColorType == Enums.ColorType.Light
                    ? _colorScheme.LightColor
                    : _colorScheme.DarkColor;

        public int UiGridRow => 8 - Rank;
        public int UiGridColumn => File - 1;
        public string FileAsLetter => "ABCDEFGH".Substring(File - 1, 1);

        public event PropertyChangedEventHandler PropertyChanged;

        public Square(int rank, int file, ColorScheme colorScheme, Enums.ColorType color)
        {
            Rank = rank;
            File = file;
            _colorScheme = colorScheme;
            ColorType = color;
        }

        public void PlacePiece(Piece piece)
        {
            Piece = piece;

            // TODO: handle captures
        }
    }
}