using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartParking.Application.Interfaces;

public class GetNearbyAvailableParkingHandler 
    : IRequestHandler<GetNearbyAvailableParkingQuery, object>
{
    private readonly IApplicationDbContext _context;

    public GetNearbyAvailableParkingHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<object> Handle(
    GetNearbyAvailableParkingQuery request,
    CancellationToken cancellationToken)
{
    // 1. Get booked slots
    var bookedSlotIds = await _context.Bookings
        .Where(b =>
            request.StartTime < b.EndTime &&
            request.EndTime > b.StartTime)
        .Select(b => b.SlotId)
        .ToListAsync(cancellationToken);

    // 2. Get available slots (DB → List)
    var availableSlots = await _context.ParkingSlots
        .Where(s => !bookedSlotIds.Contains(s.Id))
        .ToListAsync(cancellationToken);

    // 3. Get parking spaces (DB → List)
    var parkingSpaces = await _context.ParkingSpaces
        .ToListAsync(cancellationToken);

    // 4. JOIN IN MEMORY (🔥 FIX)
    var result = parkingSpaces
        .Select(ps => new
        {
            ps.Id,
            ps.Title,
            ps.Address,
            ps.Latitude,
            ps.Longitude,

            Distance = Math.Sqrt(
                Math.Pow((double)(ps.Latitude - request.Latitude), 2) +
                Math.Pow((double)(ps.Longitude - request.Longitude), 2)
            ),

            AvailableSlots = availableSlots
                .Where(s => s.ParkingSpaceId == ps.Id)
                .Select(s => new
                {
                    s.Id,
                    s.SlotNumber,
                    s.PricePerHour
                })
                .ToList()
        })
        .Where(x => x.AvailableSlots.Any())
        .OrderBy(x => x.Distance)
        .ToList();

    return result;
}
}