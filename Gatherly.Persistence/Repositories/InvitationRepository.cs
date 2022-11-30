using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;

namespace Gatherly.Persistence.Repositories
{
    internal sealed class InvitationRepository : IInvitationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public InvitationRepository(ApplicationDbContext dbContext) =>
            _dbContext = dbContext;

        public void Add(Invitation invitation) =>
            _dbContext.Set<Invitation>().Remove(invitation);
    }

}
