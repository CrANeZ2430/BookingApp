using BookingApp.Core.Domain.Members.Models;

namespace BookingApp.Application.Members.Queries.GetMembers;

public record GetMembersDto(
    string FirstName,
    string LastName,
    Roles Role,
    string Email,
    string PhoneNumber);