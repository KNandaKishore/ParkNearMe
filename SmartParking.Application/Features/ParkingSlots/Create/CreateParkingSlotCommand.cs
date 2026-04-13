using MediatR;

namespace SmartParking.Application.Features.ParkingSlots.Create
{
    public class CreateParkingSlotCommand : IRequest<Guid>
    {
        public Guid ParkingSpaceId { get; set; }
        public string SlotNumber { get; set; }
        public decimal PricePerHour { get; set; }
        public decimal? PricePerDay { get; set; }
    }
}