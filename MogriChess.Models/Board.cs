using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;

namespace MogriChess.Models
{
    public class Board : INotifyPropertyChanged
    {
        public const string COLOR_DARK = "#ADADAD";
        public const string COLOR_LIGHT = "#DDDDDD";

        private string _currentSquareColor = COLOR_DARK;

        public ObservableCollection<Square> Squares { get; } =
            new ObservableCollection<Square>();

        public event PropertyChangedEventHandler PropertyChanged;

        public Board()
        {
            PopulateBoardWithSquares();
        }

        private void PopulateBoardWithSquares()
        {
            for (int rank = 1; rank <= 8; rank++)
            {
                _currentSquareColor = rank % 2 == 0 ? COLOR_LIGHT : COLOR_DARK;

                for (int file = 1; file <= 8; file++)
                {
                    Squares.Add(new Square(rank, file, GetCurrentSquareColor()));
                }
            }
        }

        private Color GetCurrentSquareColor()
        {
            Color currentSquareColor = ColorTranslator.FromHtml(_currentSquareColor);

            // Switch to next color
            _currentSquareColor = _currentSquareColor == COLOR_LIGHT ? COLOR_DARK : COLOR_LIGHT;

            return currentSquareColor;
        }
    }
}