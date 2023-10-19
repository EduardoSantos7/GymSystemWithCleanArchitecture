using Microsoft.EntityFrameworkCore;
using GymManagement.Domain.Subscriptions;

namespace GymManagement.Infrastructure.Common.Persistence;

public class GymManagementDbContext : DbContext
{
    public DbSet<Subscription> Subscriptions { get; set; } = null!;

    public GymManagementDbContext(DbContextOptions options) : base(options)
    {
    }
}
