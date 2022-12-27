using Scrutor;

namespace Gatherly.App.Configurations
{
    public class InfrastructureServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services
                  .Scan(
                      selector => selector
                          .FromAssemblies(
                             Infrastructure.AssemblyReference.Assembly,
                             Persistence.AssemblyReference.Assembly)
                          .AddClasses(false)
                          .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                          .AsMatchingInterface()
                          .WithScopedLifetime());
        }
    }
}
