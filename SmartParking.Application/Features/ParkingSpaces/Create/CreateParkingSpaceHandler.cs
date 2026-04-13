using MediatR;
using SmartParking.Application.Interfaces;
using SmartParking.Domain.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SmartParking.Application.Features.ParkingSpaces.Create
{
    public class CreateParkingSpaceHandler : IRequestHandler<CreateParkingSpaceCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHttpContextAccessor   _httpContextAccessor;
        public CreateParkingSpaceHandler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Guid> Handle(CreateParkingSpaceCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                throw new Exception("Unauthorized");
                
            var entity = new ParkingSpace(
                Guid.Parse(userId),
                request.Title,
                request.Address,
                request.Latitude,
                request.Longitude,
                request.PricePerHour
            )
            {
                PricePerDay = request.PricePerDay // ✅ add this
            };

            await _context.ParkingSpaces.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}