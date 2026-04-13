namespace SmartParking.Domain.Entities;
public class ParkingSpace
{
    public Guid Id { get; private set; }

    public Guid OwnerId { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string Address { get; private set; } = string.Empty;

    public decimal Latitude { get; private set; }

    public decimal Longitude { get; private set; }

    public decimal PricePerHour { get; private set; }
    public decimal? PricePerDay { get; set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public ICollection<ParkingSlot> ParkingSlots { get; set; } = new List<ParkingSlot>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    private ParkingSpace() { }

    public ParkingSpace(Guid ownerId, string title, string address,
        decimal latitude, decimal longitude, decimal pricePerHour)
    {
        Id = Guid.NewGuid();
        OwnerId = ownerId;
        Title = title;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
        PricePerHour = pricePerHour;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }
}