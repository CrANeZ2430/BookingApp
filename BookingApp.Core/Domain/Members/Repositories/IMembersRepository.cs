using BookingApp.Core.Domain.Members.Models;

namespace BookingApp.Core.Domain.Members.Repositories;

public interface IMembersRepository
{
    Task<IReadOnlyCollection<Member>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default);
    Task<Member?> GetByIdAsync(Guid memberId, CancellationToken ct = default);
    Task AddAsync(Member member, CancellationToken ct = default);
    void Remove(Member member);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}