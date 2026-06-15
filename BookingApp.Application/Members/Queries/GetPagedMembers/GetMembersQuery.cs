using MediatR;

namespace BookingApp.Application.Members.Queries.GetPagedMembers;

public record GetMembersQuery(
    int Page,
    int PageSize)
    : IRequest<IReadOnlyCollection<GetMembersDto>>;