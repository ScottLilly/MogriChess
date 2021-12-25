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
        public bool IsValidDestination { get; set; }

        public string SquareColor => GetSquareColor();

        private string GetSquareColor()
        {
            if (IsSelected)
            {
                return ColorType == Enums.ColorType.Light ? "#3399FF" : "#0066DD";
            }

            if (IsValidDestination)
            {
                return ColorType == Enums.ColorType.Light ? "#99FF66" : "#66DD33";
            }

            return ColorType == Enums.ColorType.Light
                ? _colorScheme.LightColor
                : _colorScheme.DarkColor;
        }

        public int UiGridRow => 8 - Rank;
        public int UiGridColumn => File - 1;
        public string FileAsLetter => "abcdefgh".Substring(File - 1, 1);
        public string SquareShorthand => $"{FileAsLetter}{Rank}";

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

            if (!piece.IsPawn)
            {
                return;
            }

            if ((piece.ColorType == Enums.ColorType.Light && Rank == 8) ||
                (piece.ColorType == Enums.ColorType.Dark && Rank == 1))
            {
                piece.Promote();
            }
        }
    }
}