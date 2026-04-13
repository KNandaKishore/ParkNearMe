using MediatR;
public record GetAllParkingSpacesQuery() : IRequest<List<ParkingSpaceDto>>;