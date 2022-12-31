using FlightManagement.Application.Responses;
using FlightManagement.Domain.Helpers;
using MediatR;

namespace FlightManagement.Application.Queries
{
    public class
        GetAllFlightsByDepartureAndDestinationCitiesAndDepartureDateQuery : IRequest<
            Result<IReadOnlyCollection<FlightResponse>>>
    {
        public string DepartureCity { get; set; }
        public string DestinationCity { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}