using MediatR;

namespace SmartParking.Application.Features.ParkingSpaces.Nearby
{
    public record GetNearbyParkingSpacesQuery(
        decimal Latitude,
        decimal Longitude
    ) : IRequest<List<ParkingSpaceDto>>;
}