using Gatherly.Domain.Entities;

namespace Gatherly.Domain.Repositories
{
    public interface IInvitationRepository
    {
        public void Add(Invitation invitation);
    }
}
