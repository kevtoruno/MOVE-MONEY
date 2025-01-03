using System.Linq;
using Application.Core.Dtos;
using System.Collections.Generic;
using AutoMapper;
using Domain.Entities;

namespace Application.Core.Mapping
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
                    opt => opt.MapFrom(src => src.UserRole.RoleName))
                .ForMember(dest => dest.AgencyOriginId,
                    opt => opt.MapFrom(src => src.AgencyId))
                .ForMember(dest => dest.AgencyCountryId,
                opt => opt.MapFrom(src => src.Agency.CountryId));

            CreateMap<OrderForCreateDto, Order>();

            CreateMap<User, UserForRegisterDto>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<Order, OrderForReturnDto>();
            CreateMap<Customer, CustomerToReturnDto>();
            CreateMap<Agency, AgencyToReturnDto>();
            CreateMap<CustomerToCreateDto, Customer>();
            CreateMap<Customer, CustomerToCreateDto>();

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

        }
    }
}