using BookingApp.Core.Domain.Members.Models;

namespace BookingApp.Core.Domain.Members.Repositories;

public interface IMembersRepository
{
    Task<Member?> GetByIdAsync(
        Guid memberId, 
        CancellationToken ct = default);
    Task AddAsync(
        Member member, 
        CancellationToken ct = default);
    void Remove(
        Member member);
}