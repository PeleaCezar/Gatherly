using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Gatherly.Infrastructure.Authentication
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            string memberId = context.User.Claims.FirstOrDefault(
                x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

            if(!Guid.TryParse(memberId, out Guid parsedMemberId))
            {
                return;
            }

            using IServiceScope scope = _serviceScopeFactory.CreateScope();

            IPermissionService permissionService = scope.ServiceProvider
                .GetService<IPermissionService>();

            HashSet<string> permissions = await permissionService
                .GetPermissionsAsync(parsedMemberId);

            if (permissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
    }
}
