using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaseSource.Migrations
{
    /// <inheritdoc />
    public partial class updateresetpassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "2e487a96-4fbd-4b84-8875-b80b9175c77e");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "518790cd-7bca-4d18-8880-f0d5a00744d9");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "5499e708-365b-4642-b215-69d9ff43c6d0");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "6001b42e-8c5e-4ff5-ab8b-ca948db4b3c3");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "69d40ab8-cae5-4ff8-9cf5-3639bd69a0d3");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "98354782-8c02-47cb-8b4c-c12749e2e462");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "bcdc8f64-d5de-4e41-9e8e-856615c03757");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredTime",
                table: "RESET_PASSWORD",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "ROLE",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "06433fdd-3af2-4e40-bd13-3dea1a5c6269", null, "Visitor", "VISITOR" },
                    { "18f3e12e-ec89-477b-8165-6ccdbebd5af2", null, "Admin", "ADMIN" },
                    { "5b8f3b23-9f88-4d25-b83d-052b71dbbf34", null, "Customer", "CUSTOMER" },
                    { "6a3ae2d1-2832-45c0-99c0-882330147f3e", null, "Manager", "MANAGER" },
                    { "9769ce7a-ad7f-4757-9341-abd6f58c304a", null, "Collaborator", "COLLABORATOR" },
                    { "9c4af237-11a9-4c80-acf2-702f82df7c31", null, "Super Admin", "SUPER ADMIN" },
                    { "e4df13b9-4440-4a57-8532-c9d0bff9a665", null, "Staff", "STAFF" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "06433fdd-3af2-4e40-bd13-3dea1a5c6269");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "18f3e12e-ec89-477b-8165-6ccdbebd5af2");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "5b8f3b23-9f88-4d25-b83d-052b71dbbf34");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "6a3ae2d1-2832-45c0-99c0-882330147f3e");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "9769ce7a-ad7f-4757-9341-abd6f58c304a");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "9c4af237-11a9-4c80-acf2-702f82df7c31");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "e4df13b9-4440-4a57-8532-c9d0bff9a665");

            migrationBuilder.DropColumn(
                name: "ExpiredTime",
                table: "RESET_PASSWORD");

            migrationBuilder.InsertData(
                table: "ROLE",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e487a96-4fbd-4b84-8875-b80b9175c77e", null, "Customer", "CUSTOMER" },
                    { "518790cd-7bca-4d18-8880-f0d5a00744d9", null, "Staff", "STAFF" },
                    { "5499e708-365b-4642-b215-69d9ff43c6d0", null, "Visitor", "VISITOR" },
                    { "6001b42e-8c5e-4ff5-ab8b-ca948db4b3c3", null, "Super Admin", "SUPER ADMIN" },
                    { "69d40ab8-cae5-4ff8-9cf5-3639bd69a0d3", null, "Collaborator", "COLLABORATOR" },
                    { "98354782-8c02-47cb-8b4c-c12749e2e462", null, "Admin", "ADMIN" },
                    { "bcdc8f64-d5de-4e41-9e8e-856615c03757", null, "Manager", "MANAGER" }
                });
        }
    }
}
