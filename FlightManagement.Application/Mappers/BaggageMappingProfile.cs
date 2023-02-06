using AutoMapper;
using FlightManagement.Application.Commands;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;

namespace FlightManagement.Application.Mappers
{
    public class BaggageMappingProfile : Profile
    {
        public BaggageMappingProfile()
        {
            CreateMap<Baggage, BaggageResponse>().ReverseMap();
            CreateMap<Baggage, CreateBaggageCommand>().ReverseMap();
        }
    }
}