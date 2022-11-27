namespace FlightManagement.API.Dtos
{
    public class CreateBaggageDto
    {
        public double Weight { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
    }
}