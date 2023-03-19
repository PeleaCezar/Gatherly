using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Application.Members.Queries.GetMemberById;

namespace Gatherly.Application.Members.Queries.GetMembers
{
    public sealed record class GetMembersQuery(int Page, int PageSize) : IQuery<List<MemberResponse>>;

    public sealed record GetMembersCursorQuery(long Cursor, int PageSize) : IQuery<CursorResponse<List<MemberResponse>>>;

}
