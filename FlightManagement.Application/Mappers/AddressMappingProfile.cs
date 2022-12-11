using AutoMapper;
using FlightManagement.Application.Commands;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;

namespace FlightManagement.Application.Mappers
{
    public class AddressMappingProfile : Profile
    {
        public AddressMappingProfile()
        {
            CreateMap<Address, AddressResponse>().ReverseMap();
            CreateMap<Address, CreateAddressCommand>().ReverseMap();
        }
    }
}