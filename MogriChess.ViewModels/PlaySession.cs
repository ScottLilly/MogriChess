using System;
using System.ComponentModel;
using MogriChess.Models;
using MogriChess.Services;
using Newtonsoft.Json;

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

            CurrentGame.OnMoveCompleted += CurrentGame_OnMoveCompleted;
            CurrentGame.OnCheckmate += CurrentGame_OnCheckmate;

            //CurrentGame.DarkPlayerBot = new BotPlayer(Enums.ColorType.Dark);
        }

        public string GetSerializedGameState()
        {
            return BoardStateService.GetSerializedGameState(CurrentGame);
        }

        public string GetSerializedMoveHistory()
        {
            return BoardStateService.GetSerializedMoveHistory(CurrentGame);
        }

        private void CurrentGame_OnMoveCompleted(object sender, EventArgs e)
        {
            if (CurrentGame.CurrentPlayerColor == Enums.ColorType.Dark &&
                CurrentGame.DarkPlayerBot != null)
            {
                CurrentGame.MakeBotMove(CurrentGame.DarkPlayerBot);
            }

            if (CurrentGame.CurrentPlayerColor == Enums.ColorType.Light &&
                CurrentGame.LightPlayerBot != null)
            {
                CurrentGame.MakeBotMove(CurrentGame.LightPlayerBot);
            }
        }

        private void CurrentGame_OnCheckmate(object sender, EventArgs e)
        {
            GameOver?.Invoke(this, EventArgs.Empty);
        }
    }
}