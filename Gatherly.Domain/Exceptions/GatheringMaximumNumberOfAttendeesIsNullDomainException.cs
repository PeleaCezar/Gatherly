namespace Gatherly.Domain.Exceptions
{
    //Avem un minus la capitolul performanta daca implementam custom exceptions
    public sealed class GatheringMaximumNumberOfAttendeesIsNullDomainException : DomainException
    {
        public GatheringMaximumNumberOfAttendeesIsNullDomainException(string message) : base(message)
        {
        }
    }
}
