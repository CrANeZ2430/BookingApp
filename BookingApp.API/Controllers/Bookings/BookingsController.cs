using System.ComponentModel.DataAnnotations;
using BookingApp.Application.Common;
using BookingApp.Application.Requests.Bookings.Commands.CreateBooking;
using BookingApp.Application.Requests.Bookings.Commands.DeleteBooking;
using BookingApp.Application.Requests.Bookings.Commands.UpdateBooking;
using BookingApp.Application.Requests.Bookings.Queries.GetBookingById;
using BookingApp.Application.Requests.Bookings.Queries.GetBookingsByMemberId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.API.Controllers.Bookings;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BookingsController(
    ISender mediator) 
    : ControllerBase
{
    [HttpGet("/api/members/{memberId:guid}/bookings")]
    [ProducesResponseType(typeof(PageResponse<GetBookingsByMemberIdDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBookingByMemberId(
        [FromRoute] Guid memberId,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 5,
        CancellationToken ct = default)
    {
        var query = new GetBookingsByMemberIdQuery(memberId, page, pageSize);
        var response = await mediator.Send(query, ct);

        return Ok(response);
    }
    
    [HttpGet("{bookingId:guid}")]
    [ProducesResponseType(typeof(GetBookingByIdDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBookingById(
        [FromRoute] Guid bookingId,
        CancellationToken ct = default)
    {
        var query = new GetBookingByIdQuery(bookingId);
        var booking = await mediator.Send(query, ct);

        return Ok(booking);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateBooking(
        [FromBody] CreateBookingCommand command,
        CancellationToken ct = default)
    {
        var bookingId = await mediator.Send(command, ct);

        return CreatedAtAction(
            nameof(GetBookingById), 
            new { bookingId = bookingId }, 
            bookingId);
    }

    [HttpPut("{bookingId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateBooking(
        [FromRoute] Guid bookingId,
        [FromBody] UpdateBookingDto dto,
        CancellationToken ct = default)
    {
        var command = new UpdateBookingCommand(bookingId, dto);

        await mediator.Send(command, ct);
        
        return NoContent();
    }

    [HttpDelete("{bookingId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteBooking(
        [FromRoute] Guid bookingId,
        CancellationToken ct = default)
    {
        var command = new DeleteBookingCommand(bookingId);
        await mediator.Send(command, ct);

        return NoContent();
    }
}