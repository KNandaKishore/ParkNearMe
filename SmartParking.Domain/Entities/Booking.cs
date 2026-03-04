namespace SmartParking.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
        public Guid SlotId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = default!;

        public DateTime CreatedAt { get; set; }
    }
}