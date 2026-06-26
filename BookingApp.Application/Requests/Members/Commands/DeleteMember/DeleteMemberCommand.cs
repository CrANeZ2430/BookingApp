using MediatR;

namespace BookingApp.Application.Requests.Members.Commands.DeleteMember;

public record DeleteMemberCommand(
    Guid MemberId)
    : IRequest;