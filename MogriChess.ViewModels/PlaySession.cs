using System.ComponentModel;
using MogriChess.Models;
using MogriChess.Services;

namespace MogriChess.ViewModels
{
    public class PlaySession : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Game CurrentGame { get; private set; }

        public PlaySession()
        {
            CurrentGame = GameFactory.GetNewGame();
        }
    }
}