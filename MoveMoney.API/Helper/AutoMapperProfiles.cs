using AutoMapper;
using System.Linq;
using MoveMoney.API.Dtos;
using MoveMoney.API.Models;

namespace MoveMoney.API.Helper
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            //Source , Destination
            CreateMap<User, UserForDetailDto>()
                .ForMember(dest => dest.Agency, 
                    opt => opt.MapFrom(src => src.Agency.AgencyName))
                .ForMember(dest => dest.UserRole, 
                    opt => opt.MapFrom(src => src.UserRole.RoleName));

            CreateMap<User, UserForRegisterDto>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<OrderForCreateDto, Order>();
            //CreateMap<>
        }
    }
}