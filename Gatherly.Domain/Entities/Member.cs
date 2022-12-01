using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Primitives;
using Gatherly.Domain.ValueObjects;

namespace Gatherly.Domain.Entities
{
    public sealed class Member : AggregateRoot, IAuditableEntity
    {
        private Member(Guid id, Email email, FirstName firstName, LastName lastName)
         : base(id)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        private Member()
        {
        }

        public Email Email { get; set; }
        public FirstName FirstName { get; set; }
        public LastName LastName { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }


        public static Member Create(
            Guid id,
            Email email,
            FirstName firstName,
            LastName lastName
           /* bool isEmailUnique*/)
        {

            //if(!isEmailUnique)
            //{
            //    return null;
            //}
            var member = new Member(
                id,
                email,
                firstName,
                lastName);

            member.RaiseDomainEvent(new MemberRegisteredDomainEvent(
                Guid.NewGuid(),
                member.Id));

            return member;
        }

        public void ChangeName(FirstName firstName, LastName lastName)
        {
            if (!FirstName.Equals(firstName) || !LastName.Equals(lastName))
            {
                RaiseDomainEvent(new MemberNameChangedDomainEvent(
                    Guid.NewGuid(), Id));
            }

            FirstName = firstName;
            LastName = lastName;
        }
    }
}
