using FlightManagement.Application.Commands;
using FlightManagement.Application.Mappers;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, AddressResponse>
    {
        private readonly IRepository<Address> _addressRepository;

        public CreateAddressCommandHandler(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<AddressResponse> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var address = GenericMapper<Address>.Mapper.Map<Address>(request);
            if (address == null)
            {
                throw new ApplicationException("Issue with mapper");
            }

            var newAddress = await _addressRepository.AddAsync(address);

            return GenericMapper<Address>.Mapper.Map<AddressResponse>(newAddress);
        }
    }
}