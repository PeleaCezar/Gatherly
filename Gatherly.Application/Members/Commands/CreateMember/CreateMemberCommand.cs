using Gatherly.Application.Abstractions.Messaging;

namespace Gatherly.Application.Members.Commands.CreateMember
{
    public sealed record CreateMemberCommand(
        string Email,
        string FirstName,
        string LastName) : ICommand<Guid>;
}
