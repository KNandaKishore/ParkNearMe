namespace SmartParking.Domain.Entities;
public class Payment
{
    public Guid Id { get; set; }

    public Guid BookingId { get; set; }

    public string PaymentReference { get; set; } = string.Empty;

    public string PaymentMethod { get; set; } = string.Empty;

    public string PaymentStatus { get; set; } = string.Empty;

    public DateTime? PaidAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // 🔗 Navigation
    public Booking Booking { get; set; } = null!;
}