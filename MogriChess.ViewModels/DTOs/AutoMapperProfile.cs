using AutoMapper;
using MogriChess.Models;

namespace MogriChess.ViewModels.DTOs;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<MoveStruct, MoveHistoryDTO>();
        CreateMap<Square, SquareDTO>();
        CreateMap<Piece, PieceDTO>()
            .ForMember(dest => dest.Color,
                opt => opt.MapFrom(src => src.Color.ToString()));
        CreateMap<Game, GameStateDTO>()
            .ForMember(dest => dest.BoardColorScheme,
                opt => opt.MapFrom(src => src.Board.BoardColorScheme))
            .ForMember(dest => dest.PieceColorScheme,
                opt => opt.MapFrom(src => src.Board.PieceColorScheme))
            .ForMember(dest => dest.CurrentPlayerColor,
                opt => opt.MapFrom(src => src.CurrentPlayerColor.ToString()))
            .ForMember(dest => dest.Squares,
                opt => opt.MapFrom(src => src.Board.Squares.Values));
    }
}