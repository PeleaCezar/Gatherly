namespace Gatherly.Domain.Entities
{
    public class Attendee
    {
        internal Attendee(Invitation invitation) 
            : this()
        {
            GatheringId = invitation.GatheringId;
            MemberId = invitation.MemberId;
            CreateOnUtc = invitation.CreateOnUtc;
        }

        private Attendee()
        {
        }

        public Guid GatheringId { get;  private set; }

        public Guid MemberId { get; private set; }

        public DateTime CreateOnUtc { get;  private set; }
    }
}
