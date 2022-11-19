using MediatR;

namespace Gatherly.Domain.Primitives
{
    public interface IDomainEvent : INotification
    {
        public Guid Id { get; init; }
    }
}
