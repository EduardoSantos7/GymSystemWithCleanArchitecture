using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Subscriptions.Queries.GetSubscription;

public class GetSubscriptionQueryHandler : IRequestHandler<GetSubscriptionQuery, ErrorOr<Subscription>>
{
    private readonly ISubscriptionsRepository _subscriptionRepository;
    // private readonly IUnitOfWork _unitOfWork;

    public GetSubscriptionQueryHandler(ISubscriptionsRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
        // _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Subscription>> Handle(GetSubscriptionQuery query, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(query.SubscriptionId);

        return subscription is null 
            ? Error.NotFound(description: "Subscription not found")
            : subscription;
    }
}