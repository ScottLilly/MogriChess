using AutoMapper;

namespace MogriChess.Models.DTOs;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Move, MoveHistoryDTO>();
    }
}