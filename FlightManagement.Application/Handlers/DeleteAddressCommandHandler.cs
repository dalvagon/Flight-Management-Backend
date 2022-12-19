using FlightManagement.Application.Commands;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand>
    {
        private readonly IRepository<Address> _addressRepository;

        public DeleteAddressCommandHandler(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<Unit> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await _addressRepository.GetAsync(request.AddressId);
            if (address == null)
            {
                throw new ApplicationException("Issue with mapper");
            }

            _addressRepository.Delete(address);
            _addressRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}