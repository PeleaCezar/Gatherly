using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.ValueObjects;
using Microsoft.Extensions.Caching.Memory;

namespace Gatherly.Persistence.Repositories
{
    public class CachedMemberRepository : IMemberRepository
    {
        //with scrutor, here will be IMemberRepository. Same thing on a constructor
        private readonly MemberRepository _decorated;
        private readonly IMemoryCache _memoryCache;

        
        public CachedMemberRepository(MemberRepository decorated, IMemoryCache memoryCache)
        {
            _decorated = decorated;
            _memoryCache = memoryCache;
        }

        public void Add(Member member) => _decorated.Add(member);

        public Task<Member> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            string cache_key = $"member-{id}";
            return _memoryCache.GetOrCreateAsync(
                cache_key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));// expire in 2 minutes

                    return _decorated.GetByIdAsync(id, cancellationToken);
                });
        }

        public Task<Member> GetByIdWithDapperAsync(Guid id, CancellationToken cancellationToken = default)
        {
            string cache_key = $"member-{id}";
            return _memoryCache.GetOrCreateAsync(
                cache_key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));// expire in 2 minutes

                    return _decorated.GetByIdAsync(id, cancellationToken);
                });
        }

        public Task<Member> GetByEmailAsync(Email email, CancellationToken cancellationToken = default) =>
               _decorated.GetByEmailAsync(email, cancellationToken);

        public Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
        {
           return _decorated.IsEmailUniqueAsync(email, cancellationToken);
        }

        public void Update(Member member) => _decorated.Update(member);

    }
}
