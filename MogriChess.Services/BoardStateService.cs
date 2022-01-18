using MogriChess.Models;
using Newtonsoft.Json;

namespace MogriChess.Services
{
    public static class BoardStateService
    {
        public static string GetSerializedGameState(Game currentGame)
        {
            GameState state = new GameState(currentGame);

            return JsonConvert.SerializeObject(state, Formatting.Indented);
        }
    }
}