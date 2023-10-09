using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaseSource.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ADMIN_ADDRESS_ADDRESS_ID",
                table: "ADMIN");

            migrationBuilder.DropForeignKey(
                name: "FK_CUSTOMERS_ADDRESS_ADDRESS_ID",
                table: "CUSTOMERS");

            migrationBuilder.DropIndex(
                name: "IX_EMAIL_VERIFY_CUSTOMER_ID",
                table: "EMAIL_VERIFY");

            migrationBuilder.DropIndex(
                name: "IX_CUSTOMERS_ADDRESS_ID",
                table: "CUSTOMERS");

            migrationBuilder.DropIndex(
                name: "IX_ADMIN_ADDRESS_ID",
                table: "ADMIN");

            migrationBuilder.DropIndex(
                name: "IX_ACCESS_TOKEN_CUSTOMER_ID",
                table: "ACCESS_TOKEN");

            migrationBuilder.DeleteData(
                table: "ROLES",
                keyColumn: "ID",
                keyValue: "129e5e62-c185-4f41-a154-dece9191e15f");

            migrationBuilder.DeleteData(
                table: "ROLES",
                keyColumn: "ID",
                keyValue: "598de92d-7093-440c-a0ee-8bb12c081632");

            migrationBuilder.DeleteData(
                table: "ROLES",
                keyColumn: "ID",
                keyValue: "8353714b-f269-4c91-8ef0-9f6676d3e72f");

            migrationBuilder.DeleteData(
                table: "ROLES",
                keyColumn: "ID",
                keyValue: "84bf9034-901b-41f1-ba6c-65f77544513f");

            migrationBuilder.DeleteData(
                table: "ROLES",
                keyColumn: "ID",
                keyValue: "8989fa46-b9df-4859-92da-7c5006a975c0");

            migrationBuilder.DeleteData(
                table: "ROLES",
                keyColumn: "ID",
                keyValue: "a54f6ccc-7c4b-4adc-91d2-e027dbf3ea6f");

            migrationBuilder.DropColumn(
                name: "ADDRESS_ID",
                table: "CUSTOMERS");

            migrationBuilder.DropColumn(
                name: "ADDRESS_ID",
                table: "ADMIN");

          
            migrationBuilder.CreateIndex(
                name: "IX_EMAIL_VERIFY_CUSTOMER_ID",
                table: "EMAIL_VERIFY",
                column: "CUSTOMER_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ACCESS_TOKEN_CUSTOMER_ID",
                table: "ACCESS_TOKEN",
                column: "CUSTOMER_ID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EMAIL_VERIFY_CUSTOMER_ID",
                table: "EMAIL_VERIFY");

            migrationBuilder.DropIndex(
                name: "IX_ACCESS_TOKEN_CUSTOMER_ID",
                table: "ACCESS_TOKEN");

            migrationBuilder.DeleteData(
                table: "ROLES",
                keyColumn: "ID",
                keyValue: "30370dbd-e39f-467a-ad5d-5645db0b01d8");

            migrationBuilder.DeleteData(
                table: "ROLES",
                keyColumn: "ID",
                keyValue: "566f1d1d-34bb-4ce4-af18-ac9165badc7d");

            migrationBuilder.DeleteData(
                table: "ROLES",
                keyColumn: "ID",
                keyValue: "5a1e86e4-7272-4e22-a242-c00e22e4302c");

            migrationBuilder.DeleteData(
                table: "ROLES",
                keyColumn: "ID",
                keyValue: "78e6f28b-65c1-4a0e-ad56-1d1e1b6a887e");

            migrationBuilder.DeleteData(
                table: "ROLES",
                keyColumn: "ID",
                keyValue: "8c80e5e6-21ad-4a86-8a75-80931d78382e");

            migrationBuilder.DeleteData(
                table: "ROLES",
                keyColumn: "ID",
                keyValue: "8f7fc39c-c2ac-4f21-9686-d0adb983120e");

            migrationBuilder.AddColumn<string>(
                name: "ADDRESS_ID",
                table: "CUSTOMERS",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ADDRESS_ID",
                table: "ADMIN",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "ROLES",
                columns: new[] { "ID", "CREATED_BY", "ENABLE", "NAME", "UPDATED_BY" },
                values: new object[,]
                {
                    { "129e5e62-c185-4f41-a154-dece9191e15f", "admin", true, "Super Admin", "admin" },
                    { "598de92d-7093-440c-a0ee-8bb12c081632", "admin", true, "Customer", "admin" },
                    { "8353714b-f269-4c91-8ef0-9f6676d3e72f", "admin", true, "Staff", "admin" },
                    { "84bf9034-901b-41f1-ba6c-65f77544513f", "admin", true, "Admin", "admin" },
                    { "8989fa46-b9df-4859-92da-7c5006a975c0", "admin", true, "Collaborator", "admin" },
                    { "a54f6ccc-7c4b-4adc-91d2-e027dbf3ea6f", "admin", true, "Manager", "admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EMAIL_VERIFY_CUSTOMER_ID",
                table: "EMAIL_VERIFY",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMERS_ADDRESS_ID",
                table: "CUSTOMERS",
                column: "ADDRESS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ADMIN_ADDRESS_ID",
                table: "ADMIN",
                column: "ADDRESS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ACCESS_TOKEN_CUSTOMER_ID",
                table: "ACCESS_TOKEN",
                column: "CUSTOMER_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ADMIN_ADDRESS_ADDRESS_ID",
                table: "ADMIN",
                column: "ADDRESS_ID",
                principalTable: "ADDRESS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CUSTOMERS_ADDRESS_ADDRESS_ID",
                table: "CUSTOMERS",
                column: "ADDRESS_ID",
                principalTable: "ADDRESS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
