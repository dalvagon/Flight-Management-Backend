using FlightManagement.Application.Commands;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, Result>
    {
        private readonly IRepository<Address> _addressRepository;

        public DeleteAddressCommandHandler(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<Result> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await _addressRepository.GetAsync(request.AddressId);
            if (address == null)
            {
                return Result.Failure("Couldn't delete address");
            }

            _addressRepository.Delete(address);
            _addressRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}