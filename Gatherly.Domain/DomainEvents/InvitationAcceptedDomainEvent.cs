using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.DomainEvents
{
    public sealed record InvitationAcceptedDomainEvent(
        Guid Id,
        Guid InvitationId,
        Guid GatheringId) : DomainEvent(Id)
    {
    }
}
