using MediatR;

namespace SmartParking.Application.Features.ParkingSpaces.Create
{
    public class CreateParkingSpaceCommand : IRequest<Guid>
{
    public Guid OwnerId { get; set; }

    public required string Title { get; set; }
    public required string Address { get; set; }

    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }

    public decimal PricePerHour { get; set; }
    public decimal? PricePerDay { get; set; }
}
}