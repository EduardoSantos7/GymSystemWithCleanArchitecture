using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;

namespace GymManagement.Infrastructure.Subscriptions.Persistence;

public class SubscriptionsRepository : ISubscriptionsRepository
{
    private readonly static List<Subscription> _subscriptions = new ();
    public Task AddSubscriptionAsync(Subscription subscription)
    {
        _subscriptions.Add(subscription);

        return Task.CompletedTask;
    }
}