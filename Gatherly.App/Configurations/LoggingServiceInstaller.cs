namespace Gatherly.App.Configurations
{
    public class LoggingServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging();
        }
    }
}
