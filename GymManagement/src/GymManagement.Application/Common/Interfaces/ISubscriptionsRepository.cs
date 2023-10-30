using ErrorOr;
using GymManagement.Domain.Subscriptions;

namespace GymManagement.Application.Common.Interfaces;

public interface ISubscriptionsRepository
{
    Task  AddSubscriptionAsync(Subscription subscription);

    Task<Subscription?> GetSubscriptionById(Guid subscriptionId);

    Task RemoveSubscriptionAsync(Subscription subscription);
}