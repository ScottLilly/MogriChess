using System.ComponentModel;
using MogriChess.Models;

namespace MogriChess.ViewModels
{
    public class Game : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Board Board { get; } = new Board();
    }
}