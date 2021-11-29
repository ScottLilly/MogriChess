using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;

namespace MogriChess.Models
{
    public class Board : INotifyPropertyChanged
    {
        public const string COLOR_DARK = "#ADADAD";
        public const string COLOR_LIGHT = "#DDDDDD";

        private string _currentColor = COLOR_DARK;

        public ObservableCollection<Square> Squares { get; } =
            new ObservableCollection<Square>();

        public event PropertyChangedEventHandler PropertyChanged;

        public Board()
        {
            for (int rank = 1; rank <= 8; rank++)
            {
                _currentColor = rank % 2 == 0 ? COLOR_LIGHT : COLOR_DARK;

                for (int file = 1; file <= 8; file++)
                {
                    Squares.Add(new Square(rank, file, GetCurrentColor()));
                }
            }
        }

        private Color GetCurrentColor()
        {
            Color currentColor = ColorTranslator.FromHtml(_currentColor);

            // Switch to next color
            _currentColor = _currentColor == COLOR_LIGHT ? COLOR_DARK : COLOR_LIGHT;

            return currentColor;
        }
    }
}