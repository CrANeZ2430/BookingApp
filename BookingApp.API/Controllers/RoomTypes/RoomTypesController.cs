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
    public async Task<IActionResult> GetRoomTypes(
        int page = 0,
        int pageSize = 5,
        CancellationToken ct = default)
    {
        var query = new GetRoomTypesQuery(page, pageSize);
        var response = await mediator.Send(query, ct);
        
        return Ok(response);
    }
}