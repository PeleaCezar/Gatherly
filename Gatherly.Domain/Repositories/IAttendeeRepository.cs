using Gatherly.Domain.Entities;

namespace Gatherly.Domain.Repositories
{
    public interface IAttendeeRepository
    {
        public void Add(Attendee attendee);
    }
}
