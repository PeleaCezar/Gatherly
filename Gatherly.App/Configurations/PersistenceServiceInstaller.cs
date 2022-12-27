using Gatherly.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Gatherly.Persistence;

namespace Gatherly.App.Configurations
{
    public class PersistenceServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

            services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

            services.AddDbContext<ApplicationDbContext>(
                //(sp, optionsBuilder) =>
                //{
                //    var outboxInterceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>()!;
                //    var auditableInterceptor = sp.GetService<UpdateAuditableEntitiesInterceptor>()!;

                //    optionsBuilder.UseSqlServer(connectionString)
                //        .AddInterceptors(
                //            outboxInterceptor,
                //            auditableInterceptor);
                //});
                (sp, optionsBuilder) =>
                {
                    //TODO use options pattern
                    optionsBuilder.UseSqlServer(
                        configuration.GetConnectionString("Database"));
                });
        }
    }
}
