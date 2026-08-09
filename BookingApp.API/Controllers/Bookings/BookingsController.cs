using BookingApp.Application.Requests.Bookings.Commands.CreateBooking;
using BookingApp.Application.Requests.Bookings.Commands.DeleteBooking;
using BookingApp.Application.Requests.Bookings.Commands.UpdateBooking;
using BookingApp.Application.Requests.Bookings.Queries.GetBookingById;
using BookingApp.Application.Requests.Bookings.Queries.GetBookingsByMemberId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.API.Controllers.Bookings;

[ApiController]
[Route("api/[controller]")]
public class BookingsController(
    ISender mediator) 
    : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetBookingByMemberId(
        [FromQuery] Guid memberId,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 5,
        CancellationToken ct = default)
    {
        var query = new GetBookingsByMemberIdQuery(memberId, page, pageSize);
        var bookings = await mediator.Send(query, ct);

        return Ok(bookings);
    }
    
    [Authorize]
    [HttpGet("{bookingId:guid}")]
    public async Task<IActionResult> GetBookingById(
        [FromRoute] Guid bookingId,
        CancellationToken ct = default)
    {
        var query = new GetBookingByIdQuery(bookingId);
        var booking = await mediator.Send(query, ct);

        return Ok(booking);
    }
    
    [Authorize]
    [HttpPost]
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

    [Authorize]
    [HttpPut("{bookingId:guid}")]
    public async Task<IActionResult> UpdateBooking(
        [FromRoute] Guid bookingId,
        [FromBody] UpdateBookingDto dto,
        CancellationToken ct = default)
    {
        var command = new UpdateBookingCommand(bookingId, dto);

        await mediator.Send(command, ct);
        
        return NoContent();
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteBooking(
        [FromRoute] Guid bookingId,
        CancellationToken ct = default)
    {
        var command = new DeleteBookingCommand(bookingId);
        await mediator.Send(command, ct);

        return NoContent();
    }
}