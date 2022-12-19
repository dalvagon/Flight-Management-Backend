using FlightManagement.Application.Responses;
using MediatR;

namespace FlightManagement.Application.Queries
{
    public class GetAllAddressesQuery : IRequest<IReadOnlyCollection<AddressResponse>>
    {
    }
}