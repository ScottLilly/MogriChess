using System.Collections.Generic;
using AutoMapper;
using MogriChess.Models;
using MogriChess.Models.DTOs;
using Newtonsoft.Json;

namespace MogriChess.Services
{
    public static class BoardStateService
    {
        private static readonly Mapper s_mapper;

        static BoardStateService()
        {
            // Add mappings to DTO
            s_mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Move, MoveHistoryDTO>();
            }));
        }

        public static string GetSerializedGameState(Game currentGame)
        {
            return JsonConvert.SerializeObject(
                new GameState(currentGame),
                Formatting.Indented);
        }

        public static string GetSerializedMoveHistory(Game currentGame)
        {
            return JsonConvert.SerializeObject(
                s_mapper.Map<List<MoveHistoryDTO>>(currentGame.MoveHistory),
                Formatting.Indented);
        }
    }
}