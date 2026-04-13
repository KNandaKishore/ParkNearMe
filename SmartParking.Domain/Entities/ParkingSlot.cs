namespace SmartParking.Domain.Entities;
public class ParkingSlot
{
    public Guid Id { get; private set; }

    public Guid ParkingSpaceId { get; private set; }

    public string SlotNumber { get; private set; } = string.Empty;

    public bool IsOccupied { get; private set; }

    public decimal PricePerHour { get; private set; }
    public decimal? PricePerDay { get; set; }

    public DateTime CreatedAt { get; private set; }

    public ParkingSpace ParkingSpace { get; set; } = null!;

    private ParkingSlot() { }

    public ParkingSlot(Guid parkingSpaceId, string slotNumber, decimal pricePerHour)
    {
        Id = Guid.NewGuid();
        ParkingSpaceId = parkingSpaceId;
        SlotNumber = slotNumber;
        PricePerHour = pricePerHour;
        IsOccupied = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void Occupy() => IsOccupied = true;
    public void Vacate() => IsOccupied = false;
}