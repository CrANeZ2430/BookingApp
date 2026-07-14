using BookingApp.Core.Domain.Members.Models;

namespace BookingApp.Application.Requests.Members.Commands.UpdateMember;

public record UpdateMemberDto(
    string FirstName,
    string LastName,
    Roles Role,
    string Email,
    string PhoneNumber);