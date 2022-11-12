using Gatherly.Domain.Primitives;
using MediatR;

namespace Gatherly.Application.Abstractions.Messaging
{
    public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
        where TEvent : IDomainEvent
    {
    }
}
