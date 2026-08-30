using BookingApp.Application.Requests.Members.Queries.GetMemberById;
using BookingApp.Core.Exceptions;
using BookingApp.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Infrastructure.Requests.Members.Queries.GetMemberById;

public class GetMemberByIdQueryHandler(
    BookingAppDbContext dbContext)
    : IRequestHandler<GetMemberByIdQuery, GetMemberByIdDto?>
{
    public async Task<GetMemberByIdDto?> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        var member = await dbContext.Members.AsNoTracking()
            .FirstOrDefaultAsync(x => x.MemberId == 
                                      request.MemberId, cancellationToken);

        if (member is null)
            throw new NotFoundException("Given member was not found.");
        
        return new GetMemberByIdDto(
            member.MemberId,
            member.FirstName,
            member.LastName,
            member.Role,
            member.Email,
            member.PhoneNumber);
    }
}