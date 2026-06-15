using MediatR;

namespace BookingApp.Application.Members.Queries.GetMemberById;

public record GetMemberByIdQuery(
    Guid MemberId) 
    : IRequest<GetMemberByIdDto?>;