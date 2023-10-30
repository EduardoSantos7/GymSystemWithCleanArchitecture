using Microsoft.EntityFrameworkCore;
using GymManagement.Domain.Subscriptions;
using GymManagement.Application.Common.Interfaces;
using System.Reflection;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Admins;

namespace GymManagement.Infrastructure.Common.Persistence;

public class GymManagementDbContext : DbContext, IUnitOfWork
{
    public DbSet<Admin> Admins { get; set; } = null!;

    public DbSet<Subscription> Subscriptions { get; set; } = null!;

    public DbSet<Gym> Gyms {get; set; } = null!;

    public GymManagementDbContext(DbContextOptions options) : base(options)
    {
    }

    public async Task CommitChangesAsync()
    {
        await base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
