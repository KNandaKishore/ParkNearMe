using MediatR;

public class GetNearbyAvailableParkingQuery : IRequest<object>
{
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}