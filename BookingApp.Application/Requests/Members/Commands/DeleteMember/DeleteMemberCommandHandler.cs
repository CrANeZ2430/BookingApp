using BookingApp.Core.Abstractions;
using BookingApp.Core.Domain.Members.Repositories;
using MediatR;

namespace BookingApp.Application.Requests.Members.Commands.DeleteMember;

public class DeleteMemberCommandHandler(
    IMembersRepository membersRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteMemberCommand>
{
    public async Task Handle(
        DeleteMemberCommand request, 
        CancellationToken cancellationToken = default)
    {
        var member = await membersRepository.GetByIdAsync(request.MemberId, cancellationToken);
        membersRepository.Remove(member);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}