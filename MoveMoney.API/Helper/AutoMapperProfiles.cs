using AutoMapper;
using System.Linq;
using MoveMoney.API.Dtos;
using MoveMoney.API.Models;
using System.Collections.Generic;

namespace MoveMoney.API.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Source , Destination
            //This is to infere, User into UserForDetailDto and get just the names from the Agency and the role
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

            CreateMap<Order, OrderForListDto>()
                .ForMember(dest => dest.SenderName,
                opt => opt.MapFrom(src => src.Sender.FirstName + " " + src.Sender.LastName))
                .ForMember(dest => dest.ReceiverName,
                opt => opt.MapFrom(src => src.Recipient.FirstName + " " + src.Recipient.LastName))
                .ForMember(dest => dest.AgencyOriginId,
                opt => opt.MapFrom(src => src.User.Agency.Id))
                .ForMember(dest => dest.AgencyOriginName,
                opt => opt.MapFrom(src => src.User.Agency.AgencyName))
                .ForMember(dest => dest.AgencyDestinationName,
                opt => opt.MapFrom(src => src.AgencyDestination.AgencyName));

            CreateMap<List<Order>, List<OrderForListDto>>();

            CreateMap<Order, OrderForDetailDto>()
                .ForMember(dest => dest.SenderName,
                opt => opt.MapFrom(src => src.Sender.FirstName + " " + src.Sender.LastName))
                .ForMember(dest => dest.RecipientName,
                opt => opt.MapFrom(src => src.Recipient.FirstName + " " + src.Recipient.LastName))
                .ForMember(dest => dest.AgencyOriginId,
                opt => opt.MapFrom(src => src.User.Agency.Id))
                .ForMember(dest => dest.AgencyOriginName,
                opt => opt.MapFrom(src => src.User.Agency.AgencyName))
                .ForMember(dest => dest.AgencyDestinationName,
                opt => opt.MapFrom(src => src.AgencyDestination.AgencyName))
                .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.SenderTypeIdentification,
                opt => opt.MapFrom(src => src.Sender.TypeIdentification.Name))
                .ForMember(dest => dest.RecipientTypeIdentification,
                opt => opt.MapFrom(src => src.Recipient.TypeIdentification.Name));

            //CreateMap<IEnumerable<CustomerToReturnDto>, IEnumerable<Customer>>();
            //CreateMap<>
        }
    }
}