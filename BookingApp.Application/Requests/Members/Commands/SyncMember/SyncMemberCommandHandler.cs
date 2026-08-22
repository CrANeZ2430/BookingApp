using BookingApp.Core.Abstractions;
using BookingApp.Core.Domain.Members.Models;
using BookingApp.Core.Domain.Members.Repositories;
using MediatR;

namespace BookingApp.Application.Requests.Members.Commands.SyncMember;

public class SyncMemberCommandHandler(
    IMembersRepository membersRepository, 
    IUnitOfWork unitOfWork)
    : IRequestHandler<SyncMemberCommand, Guid>
{
    public async Task<Guid> Handle(
        SyncMemberCommand request, 
        CancellationToken cancellationToken = default)
    {
        var member = Member.Create(
            request.Auth0Id,
            request.FirstName,
            request.LastName,
            request.Role,
            request.Email,
            request.PhoneNumber);

        await membersRepository.AddAsync(member, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return member.MemberId;
    }
}