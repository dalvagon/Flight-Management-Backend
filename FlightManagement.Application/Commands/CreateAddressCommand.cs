using FlightManagement.Application.Responses;
using MediatR;

namespace FlightManagement.Application.Commands
{
    public class CreateAddressCommand : IRequest<AddressResponse>
    {
        public string Number { get; private set; }
        public string Street { get; private set; }
        public CityResponse City { get; private set; }
        public CountryResponse Country { get; private set; }
    }
}