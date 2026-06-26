using BookingApp.Core.Domain.Members.Repositories;
using BookingApp.Core.Exceptions;
using MediatR;

namespace BookingApp.Application.Requests.Members.Queries.GetMemberById;

public class GetMemberByIdQueryHandler(
    IMembersRepository membersRepository)
    : IRequestHandler<GetMemberByIdQuery, GetMemberByIdDto?>
{
    public async Task<GetMemberByIdDto?> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        var member = await membersRepository.GetByIdAsync(request.MemberId, cancellationToken);

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