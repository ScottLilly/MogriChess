using System.Collections.Generic;
using MogriChess.ViewModels.DTOs;
using Newtonsoft.Json;

namespace MogriChess.ViewModels;

public static class BoardStateService
{
    public static string GetSerializedGameState(Game currentGame)
    {
        return JsonConvert.SerializeObject(
            Mapper.ToGameStateDto(currentGame),
            Formatting.Indented);
    }

    public static string GetSerializedMoveHistory(Game currentGame)
    {
        return JsonConvert.SerializeObject(
            Mapper.ToMoveHistoryDtos(currentGame.MoveHistory),
            Formatting.Indented);
    }
}
