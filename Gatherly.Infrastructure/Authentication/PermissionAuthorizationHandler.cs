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

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            //first implementation with call to db and checks permissions .

            //string memberId = context.User.Claims.FirstOrDefault(
            //    x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;

            //if(!Guid.TryParse(memberId, out Guid parsedMemberId))
            //{
            //    return;
            //}

            //using IServiceScope scope = _serviceScopeFactory.CreateScope();

            //IPermissionService permissionService = scope.ServiceProvider
            //    .GetService<IPermissionService>();

            //HashSet<string> permissions = await permissionService
            //    .GetPermissionsAsync(parsedMemberId);


            // potential problems with this implementation
            //1. Claims permission pot sa ajunga foarte multe 
            // daca avem un numar foarte mare de permisiuni
            // si nu vrem ca jwt-ul nostru sa creasca foarte mult datorita acestor permisiuni,
            // fiindca asta ar creste costul benzii(bandwith costs)
            //2. Token lifetime
            // Cand cineva obtine un token cu anumite permisiuni
            // si sa spunem ca token-ul are un lifetime de 7 zile
            // asta inseamna ca in acele 7 zile acest token poate fi utilizat
            // in api-urile noastre cu acel set initial de permisiuni care au fost acordate
            // insa, in realitate, permisunile membrilor se pot schimba in acest time frame
            // aici putem avea un mecanism de revocare a tokenilor
            // când s-au schimbat permisiunile pentru acel membru
            // care sa activeze in background

            HashSet<string> permissions = context
                .User
                .Claims
                .Where(x => x.Type == CustomClaims.Permissions)
                .Select(x => x.Value)
                .ToHashSet();


            if (permissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
