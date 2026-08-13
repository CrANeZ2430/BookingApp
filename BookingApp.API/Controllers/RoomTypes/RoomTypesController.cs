using BookingApp.Application.Requests.RoomTypes.Queries.GetRoomTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.API.Controllers.RoomTypes;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RoomTypesController(
    ISender mediator)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRoomTypes(
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 5,
        [FromQuery] string? searchTerm = null,
        CancellationToken ct = default)
    {
        var query = new GetRoomTypesQuery(
            page, 
            pageSize,
            searchTerm);
        var response = await mediator.Send(query, ct);
        
        return Ok(response);
    }
}