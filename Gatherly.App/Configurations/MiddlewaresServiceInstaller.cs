using Gatherly.App.Middlewares;

namespace Gatherly.App.Configurations
{
    public class MiddlewaresServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<GlobalExceptionHandlingMiddleware>();
        }
    }
}
