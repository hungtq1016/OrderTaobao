using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaseSource.Migrations
{
    /// <inheritdoc />
    public partial class updateuserfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "USER",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "USER",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "ROLE",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "467f3cc9-0ec5-482a-aa79-adb760883532", null, "Visitor", "VISITOR" },
                    { "46fb2017-3e37-4b18-87eb-9801bc007562", null, "Super Admin", "SUPER ADMIN" },
                    { "4aaa6a73-77a7-4617-8d9a-61ba0bb69147", null, "Customer", "CUSTOMER" },
                    { "616bb54a-f3ec-4074-8190-b45189dca70e", null, "Staff", "STAFF" },
                    { "70932924-b442-40d4-8f7e-05833cf513b5", null, "Collaborator", "COLLABORATOR" },
                    { "725cdaa4-aa64-458d-a493-d68fe1e1667a", null, "Manager", "MANAGER" },
                    { "efb3eea1-9e87-4980-b241-ca41bbc3ed55", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "467f3cc9-0ec5-482a-aa79-adb760883532");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "46fb2017-3e37-4b18-87eb-9801bc007562");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "4aaa6a73-77a7-4617-8d9a-61ba0bb69147");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "616bb54a-f3ec-4074-8190-b45189dca70e");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "70932924-b442-40d4-8f7e-05833cf513b5");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "725cdaa4-aa64-458d-a493-d68fe1e1667a");

            migrationBuilder.DeleteData(
                table: "ROLE",
                keyColumn: "Id",
                keyValue: "efb3eea1-9e87-4980-b241-ca41bbc3ed55");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "USER");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "USER");

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
    }
}
