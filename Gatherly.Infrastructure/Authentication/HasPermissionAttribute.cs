using Gatherly.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Gatherly.Infrastructure.Authentication
{
    public sealed class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permission permission)
            : base(policy: permission.ToString())
        {
        }
    }
}
