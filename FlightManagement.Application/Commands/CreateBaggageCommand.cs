using FlightManagement.Application.Responses;
using MediatR;

namespace FlightManagement.Application.Commands;

public class CreateBaggageCommand : IRequest<BaggageResponse>
{
    public double Weight { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
}