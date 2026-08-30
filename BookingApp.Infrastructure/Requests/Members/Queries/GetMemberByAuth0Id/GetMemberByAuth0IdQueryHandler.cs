using BookingApp.Application.Requests.Members.Queries.GetMemberByAuth0Id;
using BookingApp.Core.Exceptions;
using BookingApp.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Infrastructure.Requests.Members.Queries.GetMemberByAuth0Id;

public class GetMemberByAuth0IdQueryHandler(
    BookingAppDbContext dbContext) 
    : IRequestHandler<GetMemberByAuth0IdQuery, GetMemberByAuth0IdDto?>
{
    public async Task<GetMemberByAuth0IdDto?> Handle(
        GetMemberByAuth0IdQuery request, 
        CancellationToken cancellationToken = default)
    {
        var member = await dbContext.Members.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Auth0Id == 
                                      request.Auth0Id, cancellationToken);

        return member is not null ? new GetMemberByAuth0IdDto(
            member.MemberId, 
            member.Auth0Id,
            member.FirstName,
            member.LastName,
            member.Role,
            member.Email,
            member.PhoneNumber) : null;
    }
}