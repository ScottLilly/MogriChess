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

            CurrentGame.OnMoveCompleted += CurrentGame_OnMoveCompleted;
            CurrentGame.OnCheckmate += CurrentGame_OnCheckmate;
        }

        public void StartGame(Enums.PlayerType lightPlayer = Enums.PlayerType.Human,
            Enums.PlayerType darkPlayer = Enums.PlayerType.Human)
        {
            CurrentGame.LightPlayerBot =
                lightPlayer == Enums.PlayerType.Bot
                    ? new BotPlayer(Enums.ColorType.Light,
                        new PieceValueCalculator(
                            new PieceValueCalculatorGenome(1,2,5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 999)))
                    : null;

            CurrentGame.DarkPlayerBot =
                darkPlayer == Enums.PlayerType.Bot
                    ? new BotPlayer(Enums.ColorType.Dark,
                        new PieceValueCalculator(
                            new PieceValueCalculatorGenome(1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 1, 2, 5, 999)))
                    : null;

            BoardFactory.PopulateBoardWithStartingPieces(CurrentGame.Board);
            CurrentGame.StartGame();
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