using BookingApp.Application.Common;
using BookingApp.Application.Requests.Rooms.Queries.GetRooms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.API.Controllers.Rooms;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RoomsController(
    ISender mediator) 
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PageResponse<GetRoomsRoomDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRooms(
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 5,
        [FromQuery] string? searchTerm = null,
        [FromQuery] int? minCapacity = null,
        [FromQuery] bool? isOperational = null,
        CancellationToken ct = default)
    {
        var query = new GetRoomsQuery(
            page, 
            pageSize, 
            searchTerm,
            minCapacity,
            isOperational);
        var response = await mediator.Send(query, ct);

        return Ok(response);
    }
}