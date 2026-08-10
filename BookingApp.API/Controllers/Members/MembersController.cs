using BookingApp.Application.Common;
using BookingApp.Application.Requests.Members.Commands.CreateMember;
using BookingApp.Application.Requests.Members.Commands.DeleteMember;
using BookingApp.Application.Requests.Members.Commands.UpdateMember;
using BookingApp.Application.Requests.Members.Queries.GetMemberById;
using BookingApp.Application.Requests.Members.Queries.GetMembers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.API.Controllers.Members;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MembersController(
    ISender mediator) 
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PageResponse<GetMembersDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMembers(
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 5,
        CancellationToken ct = default)
    {
        var query = new GetMembersQuery(page, pageSize);
        var response = await mediator.Send(query, ct);

        return Ok(response);
    }

    [HttpGet("{memberId:guid}")]
    [ProducesResponseType(typeof(GetMemberByIdDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMemberById(
        [FromRoute] Guid memberId,
        CancellationToken ct = default)
    {
        var query = new GetMemberByIdQuery(memberId);
        var member = await mediator.Send(query, ct);

        return Ok(member);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateMember(
        [FromBody] CreateMemberCommand command,
        CancellationToken ct = default)
    {
        var memberId = await mediator.Send(command, ct);

        return CreatedAtAction(
            nameof(GetMemberById), 
            new { memberId = memberId }, 
            memberId);
    }

    [HttpPut("{memberId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateMember(
        [FromRoute] Guid memberId,
        [FromBody] UpdateMemberDto dto,
        CancellationToken ct = default)
    {
        var command = new UpdateMemberCommand(memberId, dto);

        await mediator.Send(command, ct);

        return NoContent();
    }

    [HttpDelete("{memberId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteMember(
        [FromRoute] Guid memberId,
        CancellationToken ct = default)
    {
        var command = new DeleteMemberCommand(memberId);
        await mediator.Send(command, ct);

        return NoContent();
    }
}