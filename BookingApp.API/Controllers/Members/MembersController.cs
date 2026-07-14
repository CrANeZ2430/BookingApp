using BookingApp.Application.Requests.Members.Commands.CreateMember;
using BookingApp.Application.Requests.Members.Commands.DeleteMember;
using BookingApp.Application.Requests.Members.Commands.UpdateMember;
using BookingApp.Application.Requests.Members.Queries.GetMemberById;
using BookingApp.Application.Requests.Members.Queries.GetMembers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.API.Controllers.Members;

[ApiController]
[Route("api/[controller]")]
public class MembersController(
    ISender mediator) 
    : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetMembers(
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 5,
        CancellationToken ct = default)
    {
        var query = new GetMembersQuery(page, pageSize);
        var members = await mediator.Send(query, ct);

        return Ok(members);
    }

    [HttpGet("{memberId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetMemberById(
        [FromRoute] Guid memberId,
        CancellationToken ct = default)
    {
        var query = new GetMemberByIdQuery(memberId);
        var member = await mediator.Send(query, ct);

        return Ok(member);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateMember(
        [FromBody] CreateMemberCommand command,
        CancellationToken ct = default)
    {
        var memberId = await mediator.Send(command, ct);

        return CreatedAtAction(nameof(GetMemberById), new { memberId = memberId }, memberId);
    }

    [HttpPut("{memberId:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateMember(
        [FromRoute] Guid memberId,
        [FromBody] UpdateMemberDto dto,
        CancellationToken ct = default)
    {
        var request = new UpdateMemberCommand(memberId, dto);

        await mediator.Send(request, ct);

        return NoContent();
    }

    [HttpDelete("{memberId:guid}")]
    [Authorize]
    public async Task<IActionResult> RemoveMember(
        [FromRoute] Guid memberId,
        CancellationToken ct = default)
    {
        var query = new DeleteMemberCommand(memberId);
        await mediator.Send(query, ct);

        return NoContent();
    }
}