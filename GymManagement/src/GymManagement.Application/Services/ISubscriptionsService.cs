namespace GymManagement.Application.Services;

public interface ISubscriptionsService
{
    Guid CreateSubscription(string subscriptionType, Guid adminId);
}