using BookingApp.Application.Common;
using MediatR;

namespace BookingApp.Application.Requests.Members.Queries.GetMembers;

public record GetMembersQuery(
    int Page,
    int PageSize)
    : IRequest<PageResponse<GetMembersDto>>;