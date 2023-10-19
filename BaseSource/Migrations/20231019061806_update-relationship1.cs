using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaseSource.Migrations
{
    /// <inheritdoc />
    public partial class updaterelationship1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "3c697302-a6b9-42e2-96cb-112c674bf347", null, "Staff", "STAFF" },
                    { "5657c1bf-61e6-4d6d-8ee5-7d1bec862119", null, "Collaborator", "COLLABORATOR" },
                    { "60cd38f7-5059-4e16-86e7-49b79310bc7c", null, "Super Admin", "SUPER ADMIN" },
                    { "b4ecad39-036c-4989-a586-1e09bb6dddd3", null, "Manager", "MANAGER" },
                    { "bc4c3c48-d1b7-4377-bb86-120e97130ff8", null, "Admin", "ADMIN" },
                    { "f31cce57-acfc-44a9-a67a-94efdcce8b73", null, "Visitor", "VISITOR" },
                    { "fb4af28a-2007-4fe0-9ca1-401f3ddcf4eb", null, "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "3c697302-a6b9-42e2-96cb-112c674bf347");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "5657c1bf-61e6-4d6d-8ee5-7d1bec862119");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "60cd38f7-5059-4e16-86e7-49b79310bc7c");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "b4ecad39-036c-4989-a586-1e09bb6dddd3");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "bc4c3c48-d1b7-4377-bb86-120e97130ff8");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "f31cce57-acfc-44a9-a67a-94efdcce8b73");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "fb4af28a-2007-4fe0-9ca1-401f3ddcf4eb");

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
    }
}
