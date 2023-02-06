using FlightManagement.Application.Mappers;
using FlightManagement.Application.Queries;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class GetAddressCommandHandler : IRequestHandler<GetAddressQuery, Result<AddressResponse>>
    {
        private readonly IRepository<Address> _addressRepository;

        public GetAddressCommandHandler(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<Result<AddressResponse>> Handle(GetAddressQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _addressRepository.GetAsync(request.AddressId);
            if (result == null)
            {
                return Result<AddressResponse>.Failure("Couldn't find address");
            }

            var address = AddressMapper.Mapper.Map<AddressResponse>(result);

            return Result<AddressResponse>.Success(address);
        }
    }
}