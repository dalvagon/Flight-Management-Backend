using FlightManagement.Application.Commands;
using FlightManagement.Application.Mappers;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class CreatePassengerCommandHandler : IRequestHandler<CreatePassengerCommand, Result<PassengerResponse>>
    {
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<Flight> _flightRepository;
        private readonly IRepository<Allergy> _allergyRepository;
        private readonly IRepository<Passenger> _passengerRepository;

        public CreatePassengerCommandHandler(IRepository<Person> personRepository, IRepository<Flight> flightRepository,
            IRepository<Allergy> allergyRepository, IRepository<Passenger> passengerRepository)
        {
            _personRepository = personRepository;
            _flightRepository = flightRepository;
            _allergyRepository = allergyRepository;
            _passengerRepository = passengerRepository;
        }

        public async Task<Result<PassengerResponse>> Handle(CreatePassengerCommand request,
            CancellationToken cancellationToken)
        {
            var flight = await _flightRepository.GetAsync(request.FlightId);
            var person = await _personRepository.GetAsync(request.PersonId);
            if (flight == null)
            {
                return Result<PassengerResponse>.Failure("Couldn't find flight");
            }

            if (person == null)
            {
                return Result<PassengerResponse>.Failure("Couldn't find person");
            }

            var baggages = request.Baggages
                .Select(b => BaggageMapper.Mapper.Map<Baggage>(b))
                .ToList();
            var allergies = new List<Allergy>();

            async void Action(Guid id)
            {
                var allergy = await _allergyRepository.GetAsync(id);
                if (allergy != null)
                {
                    allergies.Add(allergy);
                }
            }

            request.AllergyIds.ForEach(Action);

            var result = Passenger.Create(person, flight, request.Weight, baggages, allergies);

            var newPassenger = await _passengerRepository.AddAsync(result.Entity!);
            _passengerRepository.SaveChangesAsync();
            _flightRepository.Update(flight);
            _flightRepository.SaveChangesAsync();

            var passenger = PassengerMapper.Mapper.Map<PassengerResponse>(newPassenger);

            return Result<PassengerResponse>.Success(passenger);
        }
    }
}