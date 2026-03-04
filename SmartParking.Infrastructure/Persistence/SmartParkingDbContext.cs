using Microsoft.EntityFrameworkCore;
using SmartParking.Domain.Entities;
using SmartParking.Domain.Entities;

namespace SmartParking.Infrastructure.Persistence
{
    public class SmartParkingDbContext : DbContext
    {
        public SmartParkingDbContext(DbContextOptions<SmartParkingDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ParkingSpace> ParkingSpaces { get; set; }
        public DbSet<ParkingSlot> ParkingSlots { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ChatbotLog> ChatbotLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique Email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Unique Slot per ParkingSpace
            modelBuilder.Entity<ParkingSlot>()
                .HasIndex(s => new { s.ParkingSpaceId, s.SlotNumber })
                .IsUnique();

            // Booking Index for fast conflict check
            modelBuilder.Entity<Booking>()
                .HasIndex(b => new { b.SlotId, b.StartTime, b.EndTime });
        }
    }
}