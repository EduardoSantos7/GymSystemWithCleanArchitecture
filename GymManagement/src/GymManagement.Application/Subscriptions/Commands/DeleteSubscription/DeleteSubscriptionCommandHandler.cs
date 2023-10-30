using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.DeleteSubscription;

public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, ErrorOr<Deleted>>
{
    private readonly ISubscriptionsRepository _subscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSubscriptionCommandHandler(ISubscriptionsRepository subscriptionRepository, IUnitOfWork unitOfWork)
    {
        _subscriptionRepository = subscriptionRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<Deleted>> Handle(DeleteSubscriptionCommand command, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetSubscriptionById(command.SubscriptionId).ConfigureAwait(false);

        if (subscription is null) 
        {
            return Error.NotFound(description: "Subscription not found");
        }
        await _subscriptionRepository.RemoveSubscriptionAsync(subscription);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}