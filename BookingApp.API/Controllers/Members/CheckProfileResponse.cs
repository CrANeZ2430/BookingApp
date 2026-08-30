using BookingApp.Application.Requests.Members.Queries.GetMemberByAuth0Id;

namespace BookingApp.API.Controllers.Members;

public record CheckProfileResponse(
    bool ProfileExists, 
    GetMemberByAuth0IdDto? Member);