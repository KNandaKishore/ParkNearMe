namespace SmartParking.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; private set; }   // ✅ SINGLE ID

        public Guid UserId { get; private set; }
        public Guid SlotId { get; private set; }

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public decimal TotalAmount { get; private set; }
        public string Status { get; private set; } = "Active";

        public DateTime CreatedAt { get; private set; }

        private Booking() { } // EF

        public Booking(
            Guid userId,
            Guid slotId,
            DateTime startTime,
            DateTime endTime,
            decimal totalAmount)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            SlotId = slotId;
            StartTime = startTime;
            EndTime = endTime;
            TotalAmount = totalAmount;
            Status = "Confirmed";
            CreatedAt = DateTime.UtcNow;
        }
    }
}