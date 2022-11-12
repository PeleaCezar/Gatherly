using MediatR;

namespace Gatherly.Application.Invitations.Commands.AcceptInvitation
{
    public sealed record AcceptInvitationCommand(Guid GatheringId, Guid InvitationId) : IRequest;
}
