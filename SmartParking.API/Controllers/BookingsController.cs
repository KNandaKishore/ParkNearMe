using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartParking.Application.Features.Bookings.Create;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SmartParking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();
            command.UserId = Guid.Parse(userId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [Authorize]
        [HttpPost("complete/{id}")]
        public async Task<IActionResult> Complete(Guid id)
        {
            await _mediator.Send(new CompleteBookingCommand(id));
            return Ok();
        }
    }
}