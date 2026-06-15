using BookingApp.Core.Domain.Members.Repositories;
using MediatR;

namespace BookingApp.Application.Members.Queries.GetPagedMembers;

public class GetMembersQueryHandler(
    IMembersRepository membersRepository) 
    : IRequestHandler<GetMembersQuery, IReadOnlyCollection<GetMembersDto>>
{
    public async Task<IReadOnlyCollection<GetMembersDto>> Handle(
        GetMembersQuery request, 
        CancellationToken cancellationToken = default)
    {
        if (request.Page < 0)
            throw new ArgumentOutOfRangeException(nameof(request.Page), "Page cannot be less then 0.");
        if (request.PageSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(request.PageSize), "Page size cannit be lass than 0.");
        
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