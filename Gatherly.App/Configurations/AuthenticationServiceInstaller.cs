using Gatherly.App.OptionsSetup;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Gatherly.App.Configurations
{
    public class AuthenticationServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureOptions<JwtOptionsSetup>();
            services.ConfigureOptions<JwtBearerOptionsSetup>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();         
        }
    }
}
