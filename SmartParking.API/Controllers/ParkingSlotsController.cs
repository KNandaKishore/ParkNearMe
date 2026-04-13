using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartParking.Application.Features.ParkingSlots.Create;

namespace SmartParking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParkingSlotsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ParkingSlotsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateParkingSlotCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}