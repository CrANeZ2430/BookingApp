using MediatR;

namespace BookingApp.Application.Requests.Members.Commands.UpdateMember;

public record UpdateMemberCommand(
    Guid MemberId,
    UpdateMemberDto Dto)
    : IRequest;