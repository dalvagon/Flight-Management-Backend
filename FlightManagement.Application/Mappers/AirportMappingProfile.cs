using AutoMapper;
using FlightManagement.Application.Commands;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;

namespace FlightManagement.Application.Mappers;

public class AirportMappingProfile : Profile
{
    public AirportMappingProfile()
    {
        CreateMap<Airport, AirportResponse>().ReverseMap();
        CreateMap<Airport, CreateAirportCommand>().ReverseMap();
    }
}