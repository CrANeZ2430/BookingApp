using BookingApp.Core.Domain.Members.Models;
using BookingApp.Core.Domain.Members.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Infrastructure.Database.Repositories.Members;

public class MembersRepository(BookingAppDbContext dbContext) : IMembersRepository
{
    public async Task<(IReadOnlyCollection<Member> Items, int TotalCount)> GetAsync(
        int page, 
        int pageSize, 
        CancellationToken ct = default)
    {
        var query = dbContext.Members.AsNoTracking();

        var totalCount = await query.CountAsync(ct);
        
        var members = await query.Skip(page * pageSize)
            .Take(pageSize)
            .ToArrayAsync(ct);

        return (members, totalCount);
    }

    public async Task<Member?> GetByIdAsync(
        Guid memberId, 
        CancellationToken ct = default)
    {
        return await dbContext.Members
            .FirstOrDefaultAsync(m => m.MemberId == memberId, ct);
    }

    public async Task AddAsync(
        Member member, 
        CancellationToken ct = default)
    {
        await dbContext.Members.AddAsync(member, ct);
    }

    public void Remove(
        Member member)
    {
        dbContext.Members.Remove(member);
    }
}