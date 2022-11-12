using Gatherly.Application.Abstractions.Messaging;

namespace Gatherly.Application.Members.Queries.GetMemberById
{
    public sealed record GetMemberByIdQuery(Guid MemberId) : IQuery<MemberResponse>;
}
