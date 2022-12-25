using Gatherly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Permission = Gatherly.Domain.Enums.Permission;

namespace Gatherly.Persistence.Configurations
{
    internal sealed class RolePermissionConfiguration
        : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(x => new { x.RoleId, x.PermissionId });

            builder.HasData(
                Create(Role.Registered, Permission.ReadMember),
                Create(Role.Registered, Permission.UpdateMember));
        }

        private static RolePermission Create(
            Role role, Permission permission)
        {
            return new RolePermission
            {
                RoleId = role.Id,
                PermissionId = (int)permission
            };
        }
    }
}
