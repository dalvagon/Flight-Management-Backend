﻿using FlightManagement.Application.Responses;
using FlightManagement.Domain.Helpers;
using MediatR;

namespace FlightManagement.Application.Queries
{
    public class GetAllAirportsByCityQuery : IRequest<Result<IReadOnlyCollection<AirportResponse>>>
    {
        public string CityName { get; set; }
    }
}