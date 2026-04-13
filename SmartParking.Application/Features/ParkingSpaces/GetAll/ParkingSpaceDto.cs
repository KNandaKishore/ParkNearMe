public class ParkingSpaceDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public decimal PricePerHour { get; set; }
     public double Distance { get; set; } // 🔥 NEW
}