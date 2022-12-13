using AutoMapper;
using FlightManagement.Application.Commands;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;

namespace FlightManagement.Application.Mappers;

public class FlightMappingProfile : Profile
{
    public FlightMappingProfile()
    {
        CreateMap<Flight, FlightResponse>().ReverseMap();
        CreateMap<Flight, CreateFlightCommand>().ReverseMap();
    }
}