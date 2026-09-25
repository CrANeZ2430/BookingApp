using System.Security.Claims;
using BookingApp.API.Policies;
using BookingApp.Application.Common;
using BookingApp.Application.Requests.Members.Commands.CreateMember;
using BookingApp.Application.Requests.Members.Commands.DeleteMember;
using BookingApp.Application.Requests.Members.Commands.SyncMember;
using BookingApp.Application.Requests.Members.Commands.UpdateMember;
using BookingApp.Application.Requests.Members.Queries.GetMemberByAuth0Id;
using BookingApp.Application.Requests.Members.Queries.GetMemberById;
using BookingApp.Application.Requests.Members.Queries.GetMembers;
using BookingApp.Core.Domain.Members.Models;
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
    [Authorize(Policy = "CanReadMembers")]
    [ProducesResponseType(typeof(PageResponse<GetMembersDto>), StatusCodes.Status200OK)]
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
    public async Task<IActionResult> CreateMember(
        [FromBody] CreateMemberCommand command,
        CancellationToken ct = default)
    {
        var memberId = await mediator.Send(command, ct);

        return CreatedAtAction(
            nameof(GetMemberById), 
            new { memberId }, 
            memberId);
    }

    [HttpGet("me")]
    [ProducesResponseType(typeof(CheckProfileResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CheckProfile(
        CancellationToken ct = default)
    {
        var auth0Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (auth0Id is null) return Unauthorized();
        
        var query = new GetMemberByAuth0IdQuery(auth0Id);

        var member = await mediator.Send(query, ct);

        if (member is null)
            return Ok(new CheckProfileResponse(
                ProfileExists: false,
                Member: null));

        var permissions = User.Claims
            .Where(c => c.Type == "permissions")
            .Select(c => c.Value);

        var role = permissions.Contains(Permissions.ReadMembers) ? Roles.Staff : Roles.Customer;

        if (member.Role != role)
        {
            var command = new UpdateMemberCommand(
                member.MemberId,
                new UpdateMemberDto(
                    member.FirstName,
                    member.LastName,
                    role,
                    member.Email,
                    member.PhoneNumber));
            
            await mediator.Send(command, ct);

            member = member with { Role = role };
        }

        return Ok(new CheckProfileResponse(
            ProfileExists: true, 
            Member: member));
    }
    
    [HttpPost("sync")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> CompleteProfile(
        [FromBody] SyncMemberRequest request,
        CancellationToken ct = default)
    {
        var auth0Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrEmpty(auth0Id)) return Unauthorized();
        
        var email = User.FindFirst(ClaimTypes.Email)?.Value
                    ?? User.FindFirst("https://bookingapp.com/email")?.Value;
        if (email is null) return BadRequest("Email address not provided.");
        
        var query = new GetMemberByAuth0IdQuery(auth0Id);
        
        var member = await mediator.Send(query, ct);
        if (member is not null) return BadRequest("Profile already completed.");

        var command = new SyncMemberCommand(
            auth0Id,
            request.FirstName,
            request.LastName,
            request.Role,
            email,
            request.PhoneNumber);

        var memberId = await mediator.Send(command, ct);

        return Ok(memberId);
    }

    [HttpPut("{memberId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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
    public async Task<IActionResult> DeleteMember(
        [FromRoute] Guid memberId,
        CancellationToken ct = default)
    {
        var command = new DeleteMemberCommand(memberId);
        await mediator.Send(command, ct);

        return NoContent();
    }
}