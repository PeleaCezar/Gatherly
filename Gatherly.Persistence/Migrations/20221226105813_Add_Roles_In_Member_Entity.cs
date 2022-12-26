using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gatherly.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesInMemberEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberRole_Roles_RoleId",
                table: "MemberRole");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "MemberRole",
                newName: "RolesId");

            migrationBuilder.RenameIndex(
                name: "IX_MemberRole_RoleId",
                table: "MemberRole",
                newName: "IX_MemberRole_RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberRole_Roles_RolesId",
                table: "MemberRole",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberRole_Roles_RolesId",
                table: "MemberRole");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "MemberRole",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_MemberRole_RolesId",
                table: "MemberRole",
                newName: "IX_MemberRole_RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberRole_Roles_RoleId",
                table: "MemberRole",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
