using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaseSource.Migrations
{
    /// <inheritdoc />
    public partial class updaterelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "030b47f8-c474-4800-9962-78d57afaeff8");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "1416ad8d-5c17-4fa0-818d-27e2ad2a0469");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "47bea134-c5fc-4815-8db6-426614ef36ab");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "79fc02d3-bd5a-493a-a084-294e34677184");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "a28825ba-35c7-4ebd-a771-7bd295aa884b");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "b8b8dee8-1939-4ed8-9eda-4500d4b566fc");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "ec203b2e-7fba-4a7b-a30b-82527b0c0deb");

            migrationBuilder.AddColumn<string>(
                name: "RoleId1",
                table: "AspNetUserRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "AspNetUserRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "ROLE",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e51a4d1-7599-4fee-878f-3d17148f422a", null, "Super Admin", "SUPER ADMIN" },
                    { "21ab45c9-7eb9-4cfd-9f65-71f7fb2884c0", null, "Staff", "STAFF" },
                    { "3634262c-12fc-4fcb-99b7-ea432794e81d", null, "Admin", "ADMIN" },
                    { "6bc52709-a3ec-4ed4-bfdd-5aa6b2ed717d", null, "Visitor", "VISITOR" },
                    { "7262c661-bd9a-444c-a210-4f4989f2a2c4", null, "Collaborator", "COLLABORATOR" },
                    { "a983a6a8-74fb-488a-bf04-3ea10437c3fb", null, "Customer", "CUSTOMER" },
                    { "dff84144-4c22-46b8-9d2b-a6d6a43febad", null, "Manager", "MANAGER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId1",
                table: "AspNetUserRoles",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_ROLE_RoleId1",
                table: "AspNetUserRoles",
                column: "RoleId1",
                principalTable: "ROLE",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_USER_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1",
                principalTable: "USER",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_ROLE_RoleId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_USER_UserId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_RoleId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "0e51a4d1-7599-4fee-878f-3d17148f422a");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "21ab45c9-7eb9-4cfd-9f65-71f7fb2884c0");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "3634262c-12fc-4fcb-99b7-ea432794e81d");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "6bc52709-a3ec-4ed4-bfdd-5aa6b2ed717d");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "7262c661-bd9a-444c-a210-4f4989f2a2c4");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "a983a6a8-74fb-488a-bf04-3ea10437c3fb");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "dff84144-4c22-46b8-9d2b-a6d6a43febad");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "AspNetUserRoles");

            migrationBuilder.InsertData(
                table: "ROLE",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "030b47f8-c474-4800-9962-78d57afaeff8", null, "Visitor", "VISITOR" },
                    { "1416ad8d-5c17-4fa0-818d-27e2ad2a0469", null, "Super Admin", "SUPER ADMIN" },
                    { "47bea134-c5fc-4815-8db6-426614ef36ab", null, "Staff", "STAFF" },
                    { "79fc02d3-bd5a-493a-a084-294e34677184", null, "Manager", "MANAGER" },
                    { "a28825ba-35c7-4ebd-a771-7bd295aa884b", null, "Admin", "ADMIN" },
                    { "b8b8dee8-1939-4ed8-9eda-4500d4b566fc", null, "Customer", "CUSTOMER" },
                    { "ec203b2e-7fba-4a7b-a30b-82527b0c0deb", null, "Collaborator", "COLLABORATOR" }
                });
        }
    }
}
