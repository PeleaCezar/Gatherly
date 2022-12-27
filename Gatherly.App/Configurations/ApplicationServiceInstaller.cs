using FluentValidation;
using Gatherly.Application.Behaviors;
using Gatherly.Infrastructure.Idempotence;
using MediatR;

namespace Gatherly.App.Configurations
{
    public class ApplicationServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Application.AssemblyReference.Assembly);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

            services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));

            services.AddValidatorsFromAssembly(
                Application.AssemblyReference.Assembly,
                includeInternalTypes: true);
        }
    }
}