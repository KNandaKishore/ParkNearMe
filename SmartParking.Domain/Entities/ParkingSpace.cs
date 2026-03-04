namespace SmartParking.Domain.Entities
{
    public class ParkingSpace
    {
        public int Id { get; set; }
        public Guid ParkingSpaceId { get; set; }
        public Guid OwnerId { get; set; }

        public string Title { get; set; } = default!;
        public string Address { get; set; } = default!;

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public decimal PricePerHour { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}