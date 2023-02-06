using FlightManagement.Application.Mappers;
using FlightManagement.Application.Queries;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class
        GetAllAddressesQueryHandler : IRequestHandler<GetAllAddressesQuery,
            Result<IReadOnlyCollection<AddressResponse>>>
    {
        private readonly IRepository<Address> _addressRepository;

        public GetAllAddressesQueryHandler(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<Result<IReadOnlyCollection<AddressResponse>>> Handle(GetAllAddressesQuery request,
            CancellationToken cancellationToken)
        {
            var addresses =
                AddressMapper.Mapper.Map<IReadOnlyCollection<AddressResponse>>(await _addressRepository.AllAsync());

            return Result<IReadOnlyCollection<AddressResponse>>.Success(addresses);
        }
    }
}