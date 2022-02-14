using System.Collections.Generic;
using AutoMapper;
using MogriChess.ViewModels.DTOs;
using Newtonsoft.Json;

namespace MogriChess.ViewModels;

public static class BoardStateService
{
    private static IMapper s_mapper;

    public static void Configure(IMapper mapper)
    {
        s_mapper = mapper;
    }

    public static string GetSerializedGameState(Game currentGame)
    {
        return JsonConvert.SerializeObject(
            s_mapper.Map<GameStateDTO>(currentGame),
            Formatting.Indented);
    }

    public static string GetSerializedMoveHistory(Game currentGame)
    {
        return JsonConvert.SerializeObject(
            s_mapper.Map<List<MoveHistoryDTO>>(currentGame.MoveHistory),
            Formatting.Indented);
    }
}