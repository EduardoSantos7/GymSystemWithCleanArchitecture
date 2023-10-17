using ErrorOr;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Guid>>
{
    public Task<ErrorOr<Guid>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        return Guid.NewGuid();
    }
}