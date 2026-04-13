using MediatR;

namespace SmartParking.Application.Features.Bookings.Create
{
    public class CreateBookingCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid SlotId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}