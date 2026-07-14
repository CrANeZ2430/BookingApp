using BookingApp.Core.Abstractions;
using BookingApp.Core.Domain.Members.Repositories;
using BookingApp.Core.Exceptions;
using MediatR;

namespace BookingApp.Application.Requests.Members.Commands.UpdateMember;

public class UpdateMemberCommandHandler(
    IMembersRepository membersRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateMemberCommand>
{
    public async Task Handle(
        UpdateMemberCommand request, 
        CancellationToken cancellationToken = default)
    {
        var member = await membersRepository.GetByIdAsync(request.MemberId, cancellationToken);

        if (member is null)
            throw new NotFoundException("Given member was not found.");
        
        member.Update(
            request.Dto.FirstName, 
            request.Dto.LastName, 
            request.Dto.Role, 
            request.Dto.Email, 
            request.Dto.PhoneNumber);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}