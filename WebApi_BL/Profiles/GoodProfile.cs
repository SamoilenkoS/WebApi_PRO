using AutoMapper;
using System;
using WebApi_BL.DTOs;
using WebApi_DAL.Entities;

namespace WebApi_BL.Profiles
{
    public class GoodProfile : Profile
    {
        public GoodProfile()
        {
            CreateMap<Good, GoodDto>();

            CreateMap<CreateGoodDto, Good>()
                .ForMember(x => x.CreatedAt, options => options.MapFrom(src => DateTime.Now))
                .ForMember(x => x.Title, options => options.MapFrom(src => src.Title))
                .ForMember(x => x.Price, options => options.MapFrom(src => src.Price));
        }
    }
}
