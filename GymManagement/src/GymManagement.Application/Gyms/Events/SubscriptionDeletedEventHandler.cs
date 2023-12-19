using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admin.Events;
using MediatR;

namespace GymManagement.Application.Gyms.Events;

public class SubscriptionDeletedEventHandler(IGymsRepository gymsRepository)
    : INotificationHandler<SubscriptionDeletedEvent>
{
    private readonly IGymsRepository _gymsRepository = gymsRepository;

    public async Task Handle(SubscriptionDeletedEvent notification, CancellationToken cancellationToken)
    {
        var gyms = await _gymsRepository.ListBySubscriptionIdAsync(notification.SubscriptionId);

        await _gymsRepository.RemoveRangeAsync(gyms);
    }
}
