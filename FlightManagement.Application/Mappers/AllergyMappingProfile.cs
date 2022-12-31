using AutoMapper;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;

namespace FlightManagement.Application.Mappers
{
    public class AllergyMappingProfile : Profile
    {
        public AllergyMappingProfile()
        {
            CreateMap<Allergy, AllergyResponse>().ReverseMap();
        }
    }
}