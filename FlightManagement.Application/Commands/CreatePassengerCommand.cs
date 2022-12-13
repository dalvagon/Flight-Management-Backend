namespace FlightManagement.Application.Commands;

public class CreatePassengerCommand
{
    public Guid PersonId { get; set; }
    public Guid FlightId { get; set; }
    public double Weight { get; set; }
    public List<CreateBaggageCommand> BaggageDtos { get; set; }
    public List<Guid>? AllergyIds { get; set; }
}