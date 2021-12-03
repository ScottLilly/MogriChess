using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MogriChess.Models
{
    public class Board : INotifyPropertyChanged
    {
        private Enums.ColorType _squareColorType;

        public ColorScheme BoardColorScheme { get; }
        public ColorScheme PieceColorScheme { get; }

        public ObservableCollection<Square> Squares { get; } =
            new ObservableCollection<Square>();

        public event PropertyChangedEventHandler PropertyChanged;

        public Board(ColorScheme boardColorScheme, ColorScheme piecesColorScheme)
        {
            BoardColorScheme = boardColorScheme;
            PieceColorScheme = piecesColorScheme;

            PopulateBoardWithSquares();
        }

        private void PopulateBoardWithSquares()
        {
            for (int rank = 1; rank <= 8; rank++)
            {
                _squareColorType = rank % 2 == 0 ? Enums.ColorType.Light : Enums.ColorType.Dark;

                for (int file = 1; file <= 8; file++)
                {
                    Squares.Add(new Square(rank, file, BoardColorScheme, GetCurrentSquareColor()));
                }
            }
        }

        private Enums.ColorType GetCurrentSquareColor()
        {
            Enums.ColorType currentSquareColorType = _squareColorType;

            // Switch to next color
            _squareColorType =
                _squareColorType ==
                Enums.ColorType.Light ? Enums.ColorType.Dark : Enums.ColorType.Light;

            return currentSquareColorType;
        }
    }
}