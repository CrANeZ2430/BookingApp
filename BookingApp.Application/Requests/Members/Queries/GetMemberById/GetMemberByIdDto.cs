using BookingApp.Core.Domain.Members.Models;

namespace BookingApp.Application.Requests.Members.Queries.GetMemberById;

public record GetMemberByIdDto(
    string FirstName,
    string LastName,
    Roles Role,
    string Email,
    string PhoneNumber);