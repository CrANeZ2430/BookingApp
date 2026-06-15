using BookingApp.Core.Domain.Members.Repositories;
using MediatR;

namespace BookingApp.Application.Members.Queries.GetMemberById;

public class GetMemberByIdQueryHandler(
    IMembersRepository membersRepository)
    : IRequestHandler<GetMemberByIdQuery, GetMemberByIdDto?>
{
    public async Task<GetMemberByIdDto?> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        var member = await membersRepository.GetByIdAsync(request.MemberId, cancellationToken);
        
        return new GetMemberByIdDto(
            member.FirstName,
            member.LastName,
            member.Role,
            member.Email,
            member.PhoneNumber);
    }
}