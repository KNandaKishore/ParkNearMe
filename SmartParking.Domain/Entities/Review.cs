namespace SmartParking.Domain.Entities;
public class Review
{
    public Guid Id { get; set; }

    public Guid ParkingSpaceId { get; set; }

    public Guid UserId { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // 🔗 Navigation
    public ParkingSpace ParkingSpace { get; set; } = null!;
    public User User { get; set; } = null!;
}