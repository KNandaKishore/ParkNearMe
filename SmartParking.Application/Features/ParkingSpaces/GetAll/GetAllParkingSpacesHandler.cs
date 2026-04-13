using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartParking.Application.Interfaces;
public class GetAllParkingSpacesHandler 
    : IRequestHandler<GetAllParkingSpacesQuery, List<ParkingSpaceDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllParkingSpacesHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ParkingSpaceDto>> Handle(
        GetAllParkingSpacesQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.ParkingSpaces
            .Select(p => new ParkingSpaceDto
            {
                Id = p.Id,
                Title = p.Title,
                Address = p.Address,
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                PricePerHour = p.PricePerHour
            })
            .ToListAsync(cancellationToken);
    }
}