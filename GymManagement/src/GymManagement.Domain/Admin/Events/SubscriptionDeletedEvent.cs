using GymManagement.Domain.common;

namespace GymManagement.Domain.Admin.Events;

public record SubscriptionDeletedEvent(Guid SubscriptionId) : IDomainEvent;