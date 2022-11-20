using Gatherly.Domain.Entities;

namespace Gatherly.Application.Gatherings.Queries.GetGatheringById
{
    public sealed record GatheringResponse(
        Guid Id,
        string Name,
        string? Location,
        string Creator,
        IReadOnlyCollection<AttendeeResponse> Attendees,
        IReadOnlyCollection<InvitationResponse> Invitations);
}