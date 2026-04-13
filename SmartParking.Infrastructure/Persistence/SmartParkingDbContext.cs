using Microsoft.EntityFrameworkCore;
using SmartParking.Application.Interfaces;
using SmartParking.Domain.Entities;

namespace SmartParking.Infrastructure.Persistence
{
    public class SmartParkingDbContext : DbContext, IApplicationDbContext
    {
        public SmartParkingDbContext(DbContextOptions<SmartParkingDbContext> options)
            : base(options)
        {
        }

        // ✅ MUST be DbSet (not IQueryable)
        public DbSet<ParkingSpace> ParkingSpaces { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<ParkingSlot> ParkingSlots { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ChatbotLog> ChatbotLogs { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}