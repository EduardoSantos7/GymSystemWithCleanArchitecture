using Microsoft.EntityFrameworkCore;
using GymManagement.Domain.Subscriptions;
using GymManagement.Application.Common.Interfaces;

namespace GymManagement.Infrastructure.Common.Persistence;

public class GymManagementDbContext : DbContext, IUnitOfWork
{
    public DbSet<Subscription> Subscriptions { get; set; } = null!;

    public GymManagementDbContext(DbContextOptions options) : base(options)
    {
    }

    public async Task CommitChangesAsync()
    {
        await base.SaveChangesAsync();
    }
}
