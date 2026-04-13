using Microsoft.EntityFrameworkCore;
using SmartParking.Domain.Entities;

namespace SmartParking.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ParkingSpace> ParkingSpaces { get; }
        DbSet<ParkingSlot> ParkingSlots { get; }
        DbSet<Booking> Bookings { get; }
        DbSet<User> Users { get; } 
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}