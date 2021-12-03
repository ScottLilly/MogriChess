using System.ComponentModel;
using MogriChess.Models;
using MogriChess.Services;

namespace MogriChess.ViewModels
{
    public class Game : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Board Board { get; }

        public Game()
        {
            Board = BoardFactory.GetNewGameBoard();
        }
    }
}