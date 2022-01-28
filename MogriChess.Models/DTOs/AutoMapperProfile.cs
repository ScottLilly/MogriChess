using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;

namespace MogriChess.Models.DTOs;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Move, MoveHistoryDTO>();
        CreateMap<Game, GameStateDTO>()
            .ForMember(dest => dest.BoardColorScheme,
                opt => opt.MapFrom(src => src.Board.BoardColorScheme))
            .ForMember(dest => dest.PieceColorScheme,
                opt => opt.MapFrom(src => src.Board.PieceColorScheme))
            .ForMember(dest => dest.Squares,
                opt => opt.MapFrom(src => src.Board.Squares));
    }
}