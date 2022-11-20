namespace Gatherly.Application.Gatherings.Queries.GetGatheringById
{
    public sealed record AttendeeResponse(Guid MemberId, DateTime CreatedOnUtc);
}
