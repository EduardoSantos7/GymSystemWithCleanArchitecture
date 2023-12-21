using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Domain.common;

public abstract class Entity
{
    public Guid Id { get; init; }

    protected readonly List<IDomainEvent> _domainEvents = [];

    protected Entity(Guid id) => Id = id;

    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        _domainEvents.Clear();
        return copy;
    }

    protected Entity()
    {
    }
}
