using System.ComponentModel;

namespace MogriChess.Models
{
    public class MovementIndicator : INotifyPropertyChanged
    {
        public int Squares { get; set; }

        public bool DrawOneSquareIndicator => Squares is 1 or 2;
        public bool DrawTwoSquaresIndicator => Squares == 2;
        public bool DrawInfiniteSquaresIndicator => Squares > 2;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}