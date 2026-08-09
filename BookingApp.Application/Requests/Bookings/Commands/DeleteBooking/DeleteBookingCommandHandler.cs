using BookingApp.Core.Abstractions;
using BookingApp.Core.Domain.Bookings.Repositories;
using BookingApp.Core.Exceptions;
using MediatR;

namespace BookingApp.Application.Requests.Bookings.Commands.DeleteBooking;

public class DeleteBookingCommandHandler(
    IBookingsRepository bookingsRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteBookingCommand>
{
    public async Task Handle(
        DeleteBookingCommand request, 
        CancellationToken cancellationToken = default)
    {
        var booking = await bookingsRepository.GetByIdAsync(
            request.BookingId, 
            cancellationToken);

        if (booking is null)
            throw new NotFoundException("Given booking was not found.");
        
        bookingsRepository.Remove(booking);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}