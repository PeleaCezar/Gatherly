using Gatherly.Application.Abstractions.Messaging;


namespace Gatherly.Application.Gatherings.Queries.GetGatheringById
{
    public sealed record GetGatheringByIdQuery(Guid GatheringId) : IQuery<GatheringResponse>;
}
