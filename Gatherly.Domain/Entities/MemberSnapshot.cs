namespace Gatherly.Domain.Entities
{
    public sealed class MemberSnapshot
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime? ModifiedOnUtc { get; set; }
    }
}
