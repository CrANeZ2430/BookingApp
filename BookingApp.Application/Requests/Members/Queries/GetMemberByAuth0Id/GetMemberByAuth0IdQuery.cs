using MediatR;

namespace BookingApp.Application.Requests.Members.Queries.GetMemberByAuth0Id;

public record GetMemberByAuth0IdQuery(
    string Auth0Id)
    : IRequest<GetMemberByAuth0IdDto?>;