using System;
using System.ComponentModel;
using MogriChess.Models;
using MogriChess.Services;

namespace MogriChess.ViewModels
{
    public class PlaySession : INotifyPropertyChanged
    {
        public event EventHandler GameOver;
        public event PropertyChangedEventHandler PropertyChanged;

        public Game CurrentGame { get; }

        public PlaySession()
        {
            CurrentGame = GameFactory.GetNewGame();

            CurrentGame.OnCheckmate += CurrentGame_OnCheckmate;
        }

        private void CurrentGame_OnCheckmate(object sender, EventArgs e)
        {
            GameOver?.Invoke(this, EventArgs.Empty);
        }
    }
}