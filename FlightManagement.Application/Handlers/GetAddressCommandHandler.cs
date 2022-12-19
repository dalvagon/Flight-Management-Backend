using FlightManagement.Application.Commands;
using FlightManagement.Application.Mappers;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class GetAddressCommandHandler : IRequestHandler<GetAddressCommand, AddressResponse>
    {
        private readonly IRepository<Address> _addressRepository;

        public GetAddressCommandHandler(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<AddressResponse> Handle(GetAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await _addressRepository.GetAsync(request.AddressId);

            return AddressMapper.Mapper.Map<AddressResponse>(address);
        }
    }
}