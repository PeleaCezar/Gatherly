namespace Gatherly.Domain.Exceptions
{
    //Avem un minus la capitolul performanta daca implementam custom exceptions
    public sealed class GatheringInvitationsValidBeforeInHoursIsNullDomainException : DomainException
    {
        public GatheringInvitationsValidBeforeInHoursIsNullDomainException(string message) : base(message)
        {
        }
    }
}
