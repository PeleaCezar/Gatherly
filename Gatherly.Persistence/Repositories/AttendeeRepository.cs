using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;

namespace Gatherly.Persistence.Repositories
{
    internal sealed class AttendeeRepository : IAttendeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AttendeeRepository(ApplicationDbContext dbContext) =>
            _dbContext = dbContext;

        public void Add(Attendee attendee) =>
            _dbContext.Set<Attendee>().Add(attendee);
    }
}
