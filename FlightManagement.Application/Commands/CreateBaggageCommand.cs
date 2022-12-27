namespace FlightManagement.Application.Commands
{
    public class CreateBaggageCommand
    {
        public double Weight { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
    }
}