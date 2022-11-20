using Gatherly.Application.Abstractions.Messaging;

namespace Gatherly.Application.Members.Commands.UpdateMember
{
    public sealed record UpdateMemberCommand(Guid MemberId, string FirstName, string LastName) : ICommand;

}