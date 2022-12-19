using FlightManagement.Application.Mappers;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Queries
{
    public class
        GetAllAddressesQueryHandler : IRequestHandler<GetAllAddressesQuery, IReadOnlyCollection<AddressResponse>>
    {
        private readonly IRepository<Address> _addressRepository;

        public GetAllAddressesQueryHandler(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<IReadOnlyCollection<AddressResponse>> Handle(GetAllAddressesQuery request,
            CancellationToken cancellationToken)
        {
            var addresses =
                AddressMapper.Mapper.Map<IReadOnlyCollection<AddressResponse>>(await _addressRepository.AllAsync());

            return addresses;
        }
    }
}