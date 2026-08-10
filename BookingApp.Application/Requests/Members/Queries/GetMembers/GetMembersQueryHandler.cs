using BookingApp.Application.Common;
using BookingApp.Core.Domain.Members.Repositories;
using MediatR;

namespace BookingApp.Application.Requests.Members.Queries.GetMembers;

public class GetMembersQueryHandler(
    IMembersRepository membersRepository) 
    : IRequestHandler<GetMembersQuery, PageResponse<GetMembersDto>>
{
    public async Task<PageResponse<GetMembersDto>> Handle(
        GetMembersQuery request, 
        CancellationToken cancellationToken = default)
    {
        var (members, totalCount) = await membersRepository
            .GetAsync(request.Page, request.PageSize, cancellationToken);

        var memberDtos =
            members.Select(m => 
                new GetMembersDto(
                    m.MemberId,
                    m.FirstName,
                    m.LastName,
                    m.Role,
                    m.Email,
                    m.PhoneNumber)).ToArray();
        
        return new PageResponse<GetMembersDto>(
            request.Page,
            request.PageSize,
            totalCount,
            memberDtos);
    }
}