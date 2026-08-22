using BookingApp.Core.Domain.Members.Repositories;
using MediatR;

namespace BookingApp.Application.Requests.Members.Queries.GetMemberByAuth0Id;

public class GetMemberByAuth0IdQueryHandler(
    IMembersRepository membersRepository) 
    : IRequestHandler<GetMemberByAuth0IdQuery, GetMemberByAuth0IdDto?>
{
    public async Task<GetMemberByAuth0IdDto?> Handle(
        GetMemberByAuth0IdQuery request, 
        CancellationToken cancellationToken = default)
    {
        var member = await membersRepository.GetByAuth0IdAsync(
            request.Auth0Id,
            cancellationToken);

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