using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartParking.Application.Features.ParkingSpaces.Create;
using SmartParking.Application.Features.ParkingSpaces.Nearby;

namespace SmartParking.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParkingSpacesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ParkingSpacesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllParkingSpacesQuery());
        return Ok(result);
    }

    [HttpGet("nearby")]
    public async Task<IActionResult> GetNearby([FromQuery] decimal latitude, [FromQuery] decimal longitude)
    {
        var result = await _mediator.Send(new GetNearbyParkingSpacesQuery(latitude, longitude));

        return Ok(result);
    }

     [HttpPost]
        public async Task<IActionResult> Create(CreateParkingSpaceCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("nearby-available")]
        public async Task<IActionResult> GetNearbyAvailable([FromQuery] decimal lat, [FromQuery] decimal lng, [FromQuery] DateTime startTime, [FromQuery] DateTime endTime)
        {
            var result = await _mediator.Send(new GetNearbyAvailableParkingQuery
            {
                Latitude = lat,
                Longitude = lng,
                StartTime = startTime,
                EndTime = endTime
            });

            return Ok(result);
        }
}