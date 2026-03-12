using System.Text.Json;
using MogriChess.Engine.ViewModels;

namespace MogriChess.Engine.Services;

public static class BoardStateService
{
    private static readonly JsonSerializerOptions options = new() { WriteIndented = true };

    public static string GetSerializedGameState(Game currentGame)
    {
        return JsonSerializer.Serialize(
            Mapper.ToGameStateDto(currentGame), options);
    }

    public static string GetSerializedMoveHistory(Game currentGame)
    {
        return JsonSerializer.Serialize(
            Mapper.ToMoveHistoryDtos(currentGame.MoveHistory), options);
    }
}
