using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MogriChess.Models
{
    public class Board : INotifyPropertyChanged
    {
        public const string SQUARE_COLOR_DARK = "#ADADAD";
        public const string SQUARE_COLOR_LIGHT = "#DDDDDD";

        private string _currentSquareColor = SQUARE_COLOR_DARK;

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
                _currentSquareColor = rank % 2 == 0 ? SQUARE_COLOR_LIGHT : SQUARE_COLOR_DARK;

                for (int file = 1; file <= 8; file++)
                {
                    Squares.Add(new Square(rank, file, GetCurrentSquareColor()));
                }
            }
        }

        private string GetCurrentSquareColor()
        {
            string currentSquareColor = _currentSquareColor;

            // Switch to next color
            _currentSquareColor = 
                _currentSquareColor == SQUARE_COLOR_LIGHT ? SQUARE_COLOR_DARK : SQUARE_COLOR_LIGHT;

            return currentSquareColor;
        }
    }
}