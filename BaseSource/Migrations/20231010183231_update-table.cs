using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaseSource.Migrations
{
    /// <inheritdoc />
    public partial class updatetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "USER_TOKEN");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "USER");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "USER_ROLE");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "USER_LOGIN");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "USER_CLAIM");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "ROLE");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "ROLE_CLAIM");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "USER_ROLE",
                newName: "IX_USER_ROLE_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "USER_LOGIN",
                newName: "IX_USER_LOGIN_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "USER_CLAIM",
                newName: "IX_USER_CLAIM_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "ROLE_CLAIM",
                newName: "IX_ROLE_CLAIM_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USER_TOKEN",
                table: "USER_TOKEN",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_USER",
                table: "USER",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USER_ROLE",
                table: "USER_ROLE",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_USER_LOGIN",
                table: "USER_LOGIN",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_USER_CLAIM",
                table: "USER_CLAIM",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ROLE",
                table: "ROLE",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ROLE_CLAIM",
                table: "ROLE_CLAIM",
                column: "Id");

            migrationBuilder.InsertData(
                table: "ROLE",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d0cad9c-e47b-47bf-9862-f7c205c5920b", null, "Manager", "MANAGER" },
                    { "2ec9c524-0235-410a-98ac-c590dc909348", null, "Customer", "CUSTOMER" },
                    { "5b837eaf-e14b-4edb-bca5-f90d2911d385", null, "Staff", "STAFF" },
                    { "f48ee026-fee4-44d2-bbed-5f3f01edbf48", null, "Collaborator", "COLLABORATOR" },
                    { "f4a79b83-f28e-4df9-895d-3039a05825a1", null, "Admin", "ADMIN" },
                    { "f81cc16a-3d30-4096-91c7-2ecbcf79727f", null, "Super Admin", "SUPER ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ROLE_CLAIM_ROLE_RoleId",
                table: "ROLE_CLAIM",
                column: "RoleId",
                principalTable: "ROLE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_CLAIM_USER_UserId",
                table: "USER_CLAIM",
                column: "UserId",
                principalTable: "USER",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_LOGIN_USER_UserId",
                table: "USER_LOGIN",
                column: "UserId",
                principalTable: "USER",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_ROLE_ROLE_RoleId",
                table: "USER_ROLE",
                column: "RoleId",
                principalTable: "ROLE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_ROLE_USER_UserId",
                table: "USER_ROLE",
                column: "UserId",
                principalTable: "USER",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_TOKEN_USER_UserId",
                table: "USER_TOKEN",
                column: "UserId",
                principalTable: "USER",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ROLE_CLAIM_ROLE_RoleId",
                table: "ROLE_CLAIM");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_CLAIM_USER_UserId",
                table: "USER_CLAIM");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_LOGIN_USER_UserId",
                table: "USER_LOGIN");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_ROLE_ROLE_RoleId",
                table: "USER_ROLE");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_ROLE_USER_UserId",
                table: "USER_ROLE");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_TOKEN_USER_UserId",
                table: "USER_TOKEN");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USER_TOKEN",
                table: "USER_TOKEN");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USER_ROLE",
                table: "USER_ROLE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USER_LOGIN",
                table: "USER_LOGIN");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USER_CLAIM",
                table: "USER_CLAIM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USER",
                table: "USER");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ROLE_CLAIM",
                table: "ROLE_CLAIM");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ROLE",
                table: "ROLE");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "0d0cad9c-e47b-47bf-9862-f7c205c5920b");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "2ec9c524-0235-410a-98ac-c590dc909348");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "5b837eaf-e14b-4edb-bca5-f90d2911d385");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "f48ee026-fee4-44d2-bbed-5f3f01edbf48");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "f4a79b83-f28e-4df9-895d-3039a05825a1");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "f81cc16a-3d30-4096-91c7-2ecbcf79727f");

            migrationBuilder.RenameTable(
                name: "USER_TOKEN",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "USER_ROLE",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "USER_LOGIN",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "USER_CLAIM",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "USER",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "ROLE_CLAIM",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "ROLE",
                newName: "AspNetRoles");

            migrationBuilder.RenameIndex(
                name: "IX_USER_ROLE_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_USER_LOGIN_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_USER_CLAIM_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ROLE_CLAIM_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
