using System.Reflection;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using GymManagement.Domain.common;
using GymManagement.Domain.Gyms;
using GymManagement.Domain.Subscriptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Common.Persistence;

public class GymManagementDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : DbContext(options), IUnitOfWork
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    public DbSet<Admin> Admins { get; set; } = null!;
    public DbSet<Subscription> Subscriptions { get; set; } = null!;
    public DbSet<Gym> Gyms { get; set; } = null!;

    public async Task CommitChangesAsync()
    {
        // get hold of all the domain events
        var domainEventEntities = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity.PopDomainEvents())
            .SelectMany(x => x)
            .ToList();

        AddDomainEventsToOfflineProcessingQueue(domainEventEntities);

        await SaveChangesAsync();
    }

    private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEventEntities)
    {
        // fetch a queue from http context or create a new queue if it doesn't exist
        var domainEventQueue = _httpContextAccessor.HttpContext!.Items
            .TryGetValue("DomainEventQueue", out var value) && value is Queue<IDomainEvent> existingDomainEvents ? 
            existingDomainEvents
            : new Queue<IDomainEvent>();

        // add all the domain events to the end queue
        domainEventEntities.ForEach(domainEventQueue.Enqueue);
        // store the queue in the http context
        _httpContextAccessor.HttpContext!.Items["DomainEventQueue"] = domainEventQueue;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}