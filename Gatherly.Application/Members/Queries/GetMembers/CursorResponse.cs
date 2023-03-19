namespace Gatherly.Application.Members.Queries.GetMembers
{
    public sealed record CursorResponse<T>(
        long Cursor,
        T Data);
}