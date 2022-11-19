using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.DomainEvents;

namespace Gatherly.Application.Members.Events
{
    internal sealed class PerformBackgroundCheckWhenMemberRegisteredDomainEventHandler
        : IDomainEventHandler<MemberRegisteredDomainEvent>
    {
        public Task Handle(
            MemberRegisteredDomainEvent notification,
            CancellationToken cancellationToken) =>
            Task.CompletedTask;
    }
}
