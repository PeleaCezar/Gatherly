using Gatherly.Domain.Entities;
using Gatherly.Domain.ValueObjects;

namespace Gatherly.Domain.Repositories
{
    public interface IMemberRepository
    {
        Task<Member> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Member> GetByIdWithDapperAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Member> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
      
        Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default);

        Task<List<Member>> GetMembersAsync(CancellationToken cancellationToken = default);

        void Add(Member member);

        void Update(Member member);
    }
}
