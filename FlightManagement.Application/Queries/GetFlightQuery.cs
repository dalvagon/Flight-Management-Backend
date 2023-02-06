﻿using FlightManagement.Application.Responses;
using FlightManagement.Domain.Helpers;
using MediatR;

namespace FlightManagement.Application.Queries
{
    public class GetFlightQuery : IRequest<Result<FlightResponse>>
    {
        public Guid FlightId { get; set; }
    }
}