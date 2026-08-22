using BookingApp.Core.Domain.Members.Models;
using MediatR;

namespace BookingApp.Application.Requests.Members.Commands.SyncMember;

public record SyncMemberCommand(
    string Auth0Id,
    string FirstName,
    string LastName,
    Roles Role,
    string Email,
    string PhoneNumber)
    : IRequest<Guid>;