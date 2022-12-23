using AutoMapper;
using FlightManagement.Application.Commands;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;

namespace FlightManagement.Application.Mappers
{
    public class PassengerMappingProfile : Profile
    {
        public PassengerMappingProfile()
        {
            CreateMap<Passenger, PassengerResponse>().ReverseMap();
            CreateMap<Passenger, CreatePassengerCommand>().ReverseMap();
        }
    }
}