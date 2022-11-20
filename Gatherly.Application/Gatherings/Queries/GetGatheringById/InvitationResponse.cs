using Gatherly.Domain.Enums;

namespace Gatherly.Application.Gatherings.Queries.GetGatheringById
{
    public sealed record InvitationResponse(Guid InvitationId, InvitationStatus Status);
}