namespace SmartParking.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public Guid PaymentId { get; set; }
        public Guid BookingId { get; set; }

       public string PaymentReference { get; set; } = default!;
        public string PaymentMethod { get; set; } = default!;
        public string PaymentStatus { get; set; } = default!;

        public DateTime? PaidAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}