using System.Collections.Generic;
using System.IO;
using System.Linq;
using MogriChess.Models;
using MogriChess.Models.DTOs;
using MogriChess.Services;
using MogriChess.ViewModels;
using Test.MogriChess.ViewModels.Utilities;
using Xunit;


namespace Test.MogriChess.ViewModels
{
    public class TestGameStateService
    {
        public void Test_GetSerializedGameState()
        {
            string json =
                File.ReadAllText(".\\GameStateFiles\\StartOfGame_GameState.json");

            PlaySession session = new PlaySession();
            session.StartGame(Enums.PlayerType.Human, Enums.PlayerType.Bot);

            //Assert.Equal(json, session.GetSerializedGameState());
        }
    }
}