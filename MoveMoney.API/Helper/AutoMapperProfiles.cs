using AutoMapper;
using System.Linq;
using MoveMoney.API.Dtos;
using MoveMoney.API.Models;
using System.Collections.Generic;

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
            
            CreateMap<OrderForCreateDto, Order>();

            CreateMap<User, UserForRegisterDto>();
            CreateMap<UserForRegisterDto, User>();    
            CreateMap<Order, OrderForReturnDto>();
            CreateMap<Customer, CustomerToReturnDto>();
            CreateMap<List<Customer>, List<CustomerToReturnDto>>();
            CreateMap<Agency, AgencyToReturnDto>();
            CreateMap<List<Agency>, List<AgencyToReturnDto>>();
            //CreateMap<IEnumerable<CustomerToReturnDto>, IEnumerable<Customer>>();
            //CreateMap<>
        }
    }
}