namespace Gatherly.Infrastructure.Authentication
{
    public interface IPermissionService
    {
        Task<HashSet<string>> GetPermissionsAsync(Guid memberId);
    }
}
