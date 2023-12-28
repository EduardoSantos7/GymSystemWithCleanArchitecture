using System.Linq;
using System;
namespace GymManagement.Domain.UnitTests.Subscriptions;

public class SubscriptionTests
{
    [Fact]
    public void AddGym_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        // Create a subscription
        var subscription = SubscriptionFactory.CreateSubscription();
        // Create the maximum number of gyms allowed
        var gyms = Enumerable.Range(0, subscription.GetMaxGyms() + 1)
            .Select(_ => GymsFactory.CreateGym(Guid.NewGuid()))
            .List();

        // Act
        // Add all the various gyms

        // Assert
    }
}