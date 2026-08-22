using BookingApp.Core.Domain.Members.Models;

namespace BookingApp.Application.Requests.Members.Queries.GetMemberByAuth0Id;

public record GetMemberByAuth0IdDto(
    Guid MemberId,
    string Auth0Id,
    string FirstName,
    string LastName,
    Roles Role,
    string Email,
    string PhoneNumber);