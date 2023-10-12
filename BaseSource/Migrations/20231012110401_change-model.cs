using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaseSource.Migrations
{
    /// <inheritdoc />
    public partial class changemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "USER",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "USER",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "ROLE",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13c20144-905f-4613-a941-e28f1170f2fc", null, "Staff", "STAFF" },
                    { "1f3b52b0-220a-4af1-9a93-0cab26956a74", null, "Visitor", "VISITOR" },
                    { "a30734d1-0188-408e-82b7-009a3cde15ab", null, "Manager", "MANAGER" },
                    { "b3f0d28b-8b0e-4665-9328-056446258913", null, "Collaborator", "COLLABORATOR" },
                    { "bb81f25b-e579-453a-be4e-278deb149361", null, "Super Admin", "SUPER ADMIN" },
                    { "c6acd422-57d0-49e1-94dc-d6d4268ccf6e", null, "Customer", "CUSTOMER" },
                    { "f58bb811-3c9a-49c3-a873-4da534c72a11", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "13c20144-905f-4613-a941-e28f1170f2fc");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "1f3b52b0-220a-4af1-9a93-0cab26956a74");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "a30734d1-0188-408e-82b7-009a3cde15ab");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "b3f0d28b-8b0e-4665-9328-056446258913");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "bb81f25b-e579-453a-be4e-278deb149361");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "c6acd422-57d0-49e1-94dc-d6d4268ccf6e");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "f58bb811-3c9a-49c3-a873-4da534c72a11");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "USER");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "USER");

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
        }
    }
}
