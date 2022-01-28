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
        public Enums.Color Color { get; }
        public Piece Piece { get; set; }
        public bool IsSelected { get; set; }
        public bool IsValidDestination { get; set; }

        public string SquareColor =>
            Color == Enums.Color.Light
                ? _colorScheme.LightColor
                : _colorScheme.DarkColor;

        public int UiGridRow => Constants.NumberOfRanks - Rank;
        public int UiGridColumn => File - 1;
        
        public string SquareShorthand => $"{FileAsLetter}{Rank}";
        public bool IsEmpty => Piece == null;

        public event PropertyChangedEventHandler PropertyChanged;

        public Square(int rank, int file, ColorScheme colorScheme, Enums.Color color)
        {
            Rank = rank;
            File = file;
            _colorScheme = colorScheme;
            Color = color;
        }
    }
}