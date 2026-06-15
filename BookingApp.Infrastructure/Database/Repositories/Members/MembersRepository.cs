using BookingApp.Core.Domain.Members.Models;
using BookingApp.Core.Domain.Members.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Infrastructure.Database.Repositories.Members;

public class MembersRepository(BookingAppDbContext dbContext) : IMembersRepository
{
    public async Task<IReadOnlyCollection<Member>> GetPagedAsync(int page, int pageSize, CancellationToken ct = default)
    {
        return await dbContext.Members
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToArrayAsync(ct);
    }

    public async Task<Member?> GetByIdAsync(Guid memberId, CancellationToken ct = default)
    {
        return await dbContext.Members
            .FirstOrDefaultAsync(m => m.MemberId == memberId, ct);
    }

    public async Task AddAsync(Member member, CancellationToken ct = default)
    {
        await dbContext.Members.AddAsync(member, ct);
    }

    public void Remove(Member member)
    {
        dbContext.Members.Remove(member);
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await dbContext.SaveChangesAsync(ct);
    }
}