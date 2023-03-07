using System.Data;

namespace Gatherly.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

        IDbTransaction BeginTransaction();
    }
}
