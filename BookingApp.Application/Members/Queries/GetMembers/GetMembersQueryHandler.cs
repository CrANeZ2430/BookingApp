using BookingApp.Core.Domain.Members.Repositories;
using MediatR;

namespace BookingApp.Application.Members.Queries.GetMembers;

public class GetMembersQueryHandler(
    IMembersRepository membersRepository) 
    : IRequestHandler<GetMembersQuery, IReadOnlyCollection<GetMembersDto>>
{
    public async Task<IReadOnlyCollection<GetMembersDto>> Handle(
        GetMembersQuery request, 
        CancellationToken cancellationToken = default)
    {
        var members = await membersRepository
            .GetAsync(request.Page, request.PageSize, cancellationToken);
        
        return members.Select(m =>
            new GetMembersDto(
                m.FirstName,
                m.LastName,
                m.Role,
                m.Email,
                m.PhoneNumber)).ToArray();
    }
}