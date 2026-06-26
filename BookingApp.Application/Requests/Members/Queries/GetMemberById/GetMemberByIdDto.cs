using BookingApp.Core.Domain.Members.Models;

namespace BookingApp.Application.Requests.Members.Queries.GetMemberById;

public record GetMemberByIdDto(
    Guid MemberId,
    string FirstName,
    string LastName,
    Roles Role,
    string Email,
    string PhoneNumber);