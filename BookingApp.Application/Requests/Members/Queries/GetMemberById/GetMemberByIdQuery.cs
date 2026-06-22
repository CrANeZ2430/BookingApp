using MediatR;

namespace BookingApp.Application.Requests.Members.Queries.GetMemberById;

public record GetMemberByIdQuery(
    Guid MemberId) 
    : IRequest<GetMemberByIdDto?>;