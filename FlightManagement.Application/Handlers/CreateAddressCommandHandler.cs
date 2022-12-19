using FlightManagement.Application.Commands;
using FlightManagement.Application.Mappers;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers;

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, AddressResponse>
{
    private readonly IRepository<Address> _addressRepository;
    private readonly IRepository<Country> _countryRepository;
    private readonly IRepository<City> _cityRepository;

    public CreateAddressCommandHandler(IRepository<Address> addressRepository, IRepository<Country> countryRepository,
        IRepository<City> cityRepository)
    {
        _addressRepository = addressRepository;
        _countryRepository = countryRepository;
        _cityRepository = cityRepository;
    }

    public async Task<AddressResponse> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.GetAsync(request.CityId);
        var country = await _countryRepository.GetAsync(request.CountryId);

        var address = Address.Create(request.Number, request.Street, city!, country!).Entity;
        if (address == null) throw new ApplicationException("Issue with mapper");

        var newAddress = await _addressRepository.AddAsync(address);
        _addressRepository.SaveChangesAsync();

        return AddressMapper.Mapper.Map<AddressResponse>(newAddress);
    }
}