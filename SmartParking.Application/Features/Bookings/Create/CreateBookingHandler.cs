using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartParking.Application.Interfaces;
using SmartParking.Domain.Entities;

namespace SmartParking.Application.Features.Bookings.Create
{
    public class CreateBookingHandler 
        : IRequestHandler<CreateBookingCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateBookingHandler(IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Guid> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            // 🔥 1. CHECK SLOT EXISTS
            var slot = await _context.ParkingSlots.FirstOrDefaultAsync(x => x.Id == request.SlotId, cancellationToken);

            if (slot == null)
                throw new Exception("Slot not found");

            // 🔥 2. PREVENT DOUBLE BOOKING
            var conflict = await _context.Bookings.AnyAsync(b =>
                b.SlotId == request.SlotId &&
                request.StartTime < b.EndTime &&
                request.EndTime > b.StartTime,
                cancellationToken);

            if (conflict)
                throw new Exception("Slot already booked for this time");

            // 🔥 3. CALCULATE PRICE
            var hours = (decimal)(request.EndTime - request.StartTime).TotalHours;
            var total = hours * slot.PricePerHour;

            // 🔥 4. CREATE BOOKING
          var booking = new Booking(
                request.UserId,
                request.SlotId,
                request.StartTime,
                request.EndTime,
                total
            );

await _context.Bookings.AddAsync(booking, cancellationToken);
await _context.SaveChangesAsync(cancellationToken);

return booking.Id; 
        }
    }
}