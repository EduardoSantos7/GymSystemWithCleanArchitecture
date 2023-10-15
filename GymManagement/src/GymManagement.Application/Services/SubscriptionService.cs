namespace GymManagement.Application.Services;

public class SubscriptionService : ISubscriptionsService
{
    public Guid CreateSubscription(string subscriptionType, Guid adminId)
    {
        return Guid.NewGuid();
    }
}