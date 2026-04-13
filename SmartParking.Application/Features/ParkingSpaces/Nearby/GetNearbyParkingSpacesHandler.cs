using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartParking.Application.Interfaces;

namespace SmartParking.Application.Features.ParkingSpaces.Nearby
{
    public class GetNearbyParkingSpacesHandler 
        : IRequestHandler<GetNearbyParkingSpacesQuery, List<ParkingSpaceDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetNearbyParkingSpacesHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ParkingSpaceDto>> Handle(
            GetNearbyParkingSpacesQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _context.ParkingSpaces
                .Select(p => new ParkingSpaceDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Address = p.Address,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                    PricePerHour = p.PricePerHour,

                    // 🔥 Distance Calculation
                    Distance = Math.Sqrt(
                        Math.Pow((double)(p.Latitude - request.Latitude), 2) +
                        Math.Pow((double)(p.Longitude - request.Longitude), 2)
                    )
                })
                .OrderBy(p => p.Distance)
                .Take(10)
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}