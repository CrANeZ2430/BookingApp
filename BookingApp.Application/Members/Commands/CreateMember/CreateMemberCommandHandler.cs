using BookingApp.Core.Abstractions;
using BookingApp.Core.Domain.Members.Models;
using BookingApp.Core.Domain.Members.Repositories;
using FluentValidation;
using MediatR;

namespace BookingApp.Application.Members.Commands.CreateMember;

public class CreateMemberCommandHandler(
    IMembersRepository membersRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateMemberCommand, Guid>
{
    public async Task<Guid> Handle(
        CreateMemberCommand request, 
        CancellationToken cancellationToken = default)
    {
        var member = Member.Create(
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