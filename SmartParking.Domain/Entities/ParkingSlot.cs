namespace SmartParking.Domain.Entities
{
    public class ParkingSlot
    {
         public int Id { get; set; }   // ✅ Primary Key
        public Guid SlotId { get; set; }
        public Guid ParkingSpaceId { get; set; }

        public string SlotNumber { get; set; } = string.Empty;

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}