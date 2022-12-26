using Gatherly.Domain.Entities;
using Gatherly.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.Infrastructure.Authentication
{
    public class PermissionService : IPermissionService
    {
        private readonly ApplicationDbContext _context;

        public PermissionService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<HashSet<string>> GetPermissionsAsync(Guid memberId)
        {
            ICollection<Role>[] roles = await _context.Set<Member>()
                 .Include(x => x.Roles)
                 .ThenInclude(x => x.Permissions)
                 .Where(x => x.Id == memberId)
                 .Select(x => x.Roles)
                 .ToArrayAsync();

            return roles
                .SelectMany(x => x)
                .SelectMany(x => x.Permissions)
                .Select(x => x.Name)
                .ToHashSet();
        }
    }
}
