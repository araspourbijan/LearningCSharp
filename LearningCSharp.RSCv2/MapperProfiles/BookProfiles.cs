using AutoMapper;
using Shared.Dtos;
using Shared.Models;

namespace LearningCSharp.RSCv2.MapperProfiles;

public class BookDtoProfile : Profile
{
    public BookDtoProfile()
    {
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price + " $"))
            .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock))
            ;
    }
}
