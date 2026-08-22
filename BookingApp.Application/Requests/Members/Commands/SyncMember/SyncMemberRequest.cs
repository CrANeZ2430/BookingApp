using BookingApp.Core.Domain.Members.Models;

namespace BookingApp.Application.Requests.Members.Commands.SyncMember;

public record SyncMemberRequest(
    string FirstName,
    string LastName,
    string PhoneNumber);