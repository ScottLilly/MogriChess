using System.ComponentModel;

namespace MogriChess.Models
{
    public class Square : INotifyPropertyChanged
    {
        private ColorScheme _colorScheme;

        private string FileAsLetter =>
            "abcdefgh".Substring(File - 1, 1);

        public int Rank { get; }
        public int File { get; }
        public Enums.ColorType ColorType { get; }
        public Piece Piece { get; set; }
        public bool IsSelected { get; set; }
        public bool IsValidDestination { get; set; }

        public string SquareColor =>
            ColorType == Enums.ColorType.Light
                ? _colorScheme.LightColor
                : _colorScheme.DarkColor;

        public int UiGridRow => Constants.NumberOfRanks - Rank;
        public int UiGridColumn => File - 1;
        public string SquareShorthand => $"{FileAsLetter}{Rank}";

        public event PropertyChangedEventHandler PropertyChanged;

        public Square(int rank, int file, ColorScheme colorScheme, Enums.ColorType color)
        {
            Rank = rank;
            File = file;
            _colorScheme = colorScheme;
            ColorType = color;
        }
    }
}