using MediatR;
using SmartParking.Application.Interfaces;
using SmartParking.Domain.Entities;

namespace SmartParking.Application.Features.ParkingSlots.Create
{
    public class CreateParkingSlotHandler 
        : IRequestHandler<CreateParkingSlotCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateParkingSlotHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(
            CreateParkingSlotCommand request, 
            CancellationToken cancellationToken)
        {
            var slot = new ParkingSlot(
                request.ParkingSpaceId,
                request.SlotNumber,
                request.PricePerHour
            );

            await _context.ParkingSlots.AddAsync(slot, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return slot.Id;
        }
    }
}