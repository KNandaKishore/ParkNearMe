namespace SmartParking.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public Guid ReviewId { get; set; }
        public Guid ParkingSpaceId { get; set; }
        public Guid UserId { get; set; }

        public int Rating { get; set; }
        public string Comment { get; set; } = default!;

        public DateTime CreatedAt { get; set; }
    }
}