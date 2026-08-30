using BookingApp.Application.Common;
using BookingApp.Application.Requests.Members.Queries.GetMembers;
using BookingApp.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Infrastructure.Requests.Members.Queries.GetMembers;

public class GetMembersQueryHandler(
    BookingAppDbContext dbContext) 
    : IRequestHandler<GetMembersQuery, PageResponse<GetMembersDto>>
{
    public async Task<PageResponse<GetMembersDto>> Handle(
        GetMembersQuery request, 
        CancellationToken cancellationToken = default)
    {
        var query = dbContext.Members.AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var members = await query
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .Skip(request.Page * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new GetMembersDto(
                x.MemberId,
                x.FirstName,
                x.LastName,
                x.Role,
                x.Email,
                x.PhoneNumber))
            .ToArrayAsync(cancellationToken);
        
        return new PageResponse<GetMembersDto>(
            request.Page,
            request.PageSize,
            totalCount,
            members);
    }
}