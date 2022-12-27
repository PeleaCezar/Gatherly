using Gatherly.Domain.Repositories;
using Gatherly.Persistence.Repositories;

namespace Gatherly.App.Configurations
{
    public class CachingServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            //when someone injects IMemberRepository from a Constructor, they are going to get an instance of CachedMemberREpository
            //if you recall CachedMemberRepository is injecting memberRepository inside of its constructor
            services.AddScoped<MemberRepository>();
            services.AddScoped<IMemberRepository, CachedMemberRepository>();

            services.AddMemoryCache();

            // second aproach
            //builder.Services.AddScoped<IMemberRepository>(provider =>
            //{
            //    var memberRepository = provider.GetService<MemberRepository>();

            //    return new CachedMemberRepository(
            //        memberRepository,
            //        provider.GetService<IMemoryCache>()!);
            //});

            //third aproach with Scrutor
            //builder.Services.AddScoped<IMemberRepository, MemberRepository>();
            //builder.Services.Decorate<IMemberRepository, CachedMemberRepository>();
        }
    }
}
