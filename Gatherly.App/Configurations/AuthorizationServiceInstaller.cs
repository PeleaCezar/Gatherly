using Gatherly.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Gatherly.App.Configurations
{
    public class AuthorizationServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        }
    }
}
