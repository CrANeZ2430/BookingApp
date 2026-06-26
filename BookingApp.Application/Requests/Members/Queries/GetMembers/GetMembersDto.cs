using BookingApp.Core.Domain.Members.Models;

namespace BookingApp.Application.Requests.Members.Queries.GetMembers;

public record GetMembersDto(
    Guid MemberId,
    string FirstName,
    string LastName,
    Roles Role,
    string Email,
    string PhoneNumber);