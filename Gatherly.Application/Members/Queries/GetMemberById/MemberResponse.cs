namespace Gatherly.Application.Members.Queries.GetMemberById
{
    public sealed record MemberResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName);
}
