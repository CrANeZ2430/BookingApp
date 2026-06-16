using MediatR;

namespace BookingApp.Application.Members.Queries.GetMembers;

public record GetMembersQuery(
    int Page,
    int PageSize)
    : IRequest<IReadOnlyCollection<GetMembersDto>>;