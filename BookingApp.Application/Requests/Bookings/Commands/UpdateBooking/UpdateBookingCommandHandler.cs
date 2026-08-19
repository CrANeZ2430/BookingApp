using BookingApp.Core.Abstractions;
using BookingApp.Core.Domain.Bookings.Repositories;
using BookingApp.Core.Domain.Rooms.Repositories;
using BookingApp.Core.Exceptions;
using MediatR;

namespace BookingApp.Application.Requests.Bookings.Commands.UpdateBooking;

public class UpdateBookingCommandHandler(
    IBookingsRepository bookingsRepository,
    IRoomsRepository roomsRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateBookingCommand>
{
    public async Task Handle(
        UpdateBookingCommand request, 
        CancellationToken cancellationToken = default)
    {
        var booking = await bookingsRepository.GetByIdAsync(
            request.BookingId,
            cancellationToken);
        if (booking is null)
            throw new NotFoundException("Given booking was not found.");
        
        var room = await roomsRepository.GetByIdAsync(
            request.Dto.RoomId, 
            cancellationToken);
        if (room is null)
            throw new NotFoundException("Given room was not found.");
        if (!room.IsOperational)
            throw new BadRequestException(
                "Given room is under renovation.", 
                new Dictionary<string, string[]>(){
                    {
                        nameof(request.Dto.RoomId),
                        ["Given room is under renovation."]
                    }});
        if (request.Dto.AttendeeCount > room.Capacity)
            throw new BadRequestException(
                $"The room capacity is {room.Capacity}, but you requested {request.Dto.AttendeeCount} attendees.",
                new Dictionary<string, string[]>(){
                {
                    nameof(request.Dto.AttendeeCount),
                    [$"The room capacity is {room.Capacity}, but you requested {request.Dto.AttendeeCount} attendees."]
                }});
        if (await bookingsRepository.HasOverlappingAsync(
                request.Dto.RoomId, 
                request.Dto.StartTime, 
                request.Dto.EndTime,
                cancellationToken,
                request.BookingId))
            throw new BadRequestException(
                "Booking time isn't available",
                new Dictionary<string, string[]>(){
                {
                    nameof(request.Dto.StartTime),
                    ["Booking time isn't available"]
                },
                {
                    nameof(request.Dto.EndTime),
                    ["Booking time isn't available"]
                }});
        
        booking.Update(
            request.Dto.AttendeeCount,
            request.Dto.StartTime,
            request.Dto.EndTime,
            request.Dto.RoomId);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}