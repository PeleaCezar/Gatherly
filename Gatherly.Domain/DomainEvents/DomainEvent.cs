using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.DomainEvents
{
    public abstract record DomainEvent(Guid Id) : IDomainEvent;
}
