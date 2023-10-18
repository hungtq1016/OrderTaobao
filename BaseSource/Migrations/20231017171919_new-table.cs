using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaseSource.Migrations
{
    /// <inheritdoc />
    public partial class newtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AUTH_HISTORY_CUSTOMERS_CUSTOMER_ID",
                table: "AUTH_HISTORY");

            migrationBuilder.DropForeignKey(
                name: "FK_CUSTOMER_HISTORY_CUSTOMERS_CUSTOMER_ID",
                table: "CUSTOMER_HISTORY");

            migrationBuilder.DropForeignKey(
                name: "FK_NOTIFICATIONS_CUSTOMERS_CUSTOMER_ID",
                table: "NOTIFICATIONS");

            migrationBuilder.DropForeignKey(
                name: "FK_ORDERS_CUSTOMERS_CUSTOMER_ID",
                table: "ORDERS");

            migrationBuilder.DropTable(
                name: "ACCESS_TOKEN");

            migrationBuilder.DropTable(
                name: "EMAIL_VERIFY");

            migrationBuilder.DropTable(
                name: "CUSTOMERS");

            migrationBuilder.DropIndex(
                name: "IX_ORDERS_CUSTOMER_ID",
                table: "ORDERS");

            migrationBuilder.DropIndex(
                name: "IX_NOTIFICATIONS_CUSTOMER_ID",
                table: "NOTIFICATIONS");

            migrationBuilder.DropIndex(
                name: "IX_CUSTOMER_HISTORY_CUSTOMER_ID",
                table: "CUSTOMER_HISTORY");

            migrationBuilder.DropIndex(
                name: "IX_AUTH_HISTORY_CUSTOMER_ID",
                table: "AUTH_HISTORY");

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
                name: "CUSTOMER_ID",
                table: "ORDERS");

            migrationBuilder.DropColumn(
                name: "CUSTOMER_ID",
                table: "NOTIFICATIONS");

            migrationBuilder.DropColumn(
                name: "CUSTOMER_ID",
                table: "CUSTOMER_HISTORY");

            migrationBuilder.DropColumn(
                name: "CUSTOMER_ID",
                table: "AUTH_HISTORY");

            migrationBuilder.RenameColumn(
                name: "UPDATED_AT",
                table: "CUSTOMER_HISTORY",
                newName: "MODIFIED_AT");

            migrationBuilder.AddColumn<string>(
                name: "USER_ID",
                table: "ORDERS",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "USER_ID",
                table: "NOTIFICATIONS",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NEW_VALUE",
                table: "CUSTOMER_HISTORY",
                type: "nvarchar",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "USER_ID",
                table: "CUSTOMER_HISTORY",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "USER_ID",
                table: "AUTH_HISTORY",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PROVINCES",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    SLUG = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    TYPE = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    TYPE_SLUG = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    CODE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROVINCES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RESET_PASSWORD",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IS_VERIFY = table.Column<bool>(type: "bit", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESET_PASSWORD", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RESET_PASSWORD_USER_EMAIL",
                        column: x => x.EMAIL,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DISTRICTS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    PROVINCE_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    SLUG = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    TYPE = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    TYPE_SLUG = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    CODE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DISTRICTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DISTRICTS_PROVINCES_PROVINCE_ID",
                        column: x => x.PROVINCE_ID,
                        principalTable: "PROVINCES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WARDS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    DISTRICT_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    SLUG = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    TYPE = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    TYPE_SLUG = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    CODE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WARDS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WARDS_DISTRICTS_DISTRICT_ID",
                        column: x => x.DISTRICT_ID,
                        principalTable: "DISTRICTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ADDRESS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    STREET = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    WARD_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADDRESS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ADDRESS_WARDS_WARD_ID",
                        column: x => x.WARD_ID,
                        principalTable: "WARDS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_USER_ID",
                table: "ORDERS",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICATIONS_USER_ID",
                table: "NOTIFICATIONS",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMER_HISTORY_USER_ID",
                table: "CUSTOMER_HISTORY",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_HISTORY_USER_ID",
                table: "AUTH_HISTORY",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ADDRESS_WARD_ID",
                table: "ADDRESS",
                column: "WARD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DISTRICTS_PROVINCE_ID",
                table: "DISTRICTS",
                column: "PROVINCE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESET_PASSWORD_EMAIL",
                table: "RESET_PASSWORD",
                column: "EMAIL");

            migrationBuilder.CreateIndex(
                name: "IX_WARDS_DISTRICT_ID",
                table: "WARDS",
                column: "DISTRICT_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AUTH_HISTORY_USER_USER_ID",
                table: "AUTH_HISTORY",
                column: "USER_ID",
                principalTable: "USER",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CUSTOMER_HISTORY_USER_USER_ID",
                table: "CUSTOMER_HISTORY",
                column: "USER_ID",
                principalTable: "USER",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NOTIFICATIONS_USER_USER_ID",
                table: "NOTIFICATIONS",
                column: "USER_ID",
                principalTable: "USER",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ORDERS_USER_USER_ID",
                table: "ORDERS",
                column: "USER_ID",
                principalTable: "USER",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AUTH_HISTORY_USER_USER_ID",
                table: "AUTH_HISTORY");

            migrationBuilder.DropForeignKey(
                name: "FK_CUSTOMER_HISTORY_USER_USER_ID",
                table: "CUSTOMER_HISTORY");

            migrationBuilder.DropForeignKey(
                name: "FK_NOTIFICATIONS_USER_USER_ID",
                table: "NOTIFICATIONS");

            migrationBuilder.DropForeignKey(
                name: "FK_ORDERS_USER_USER_ID",
                table: "ORDERS");

            migrationBuilder.DropTable(
                name: "ADDRESS");

            migrationBuilder.DropTable(
                name: "RESET_PASSWORD");

            migrationBuilder.DropTable(
                name: "WARDS");

            migrationBuilder.DropTable(
                name: "DISTRICTS");

            migrationBuilder.DropTable(
                name: "PROVINCES");

            migrationBuilder.DropIndex(
                name: "IX_ORDERS_USER_ID",
                table: "ORDERS");

            migrationBuilder.DropIndex(
                name: "IX_NOTIFICATIONS_USER_ID",
                table: "NOTIFICATIONS");

            migrationBuilder.DropIndex(
                name: "IX_CUSTOMER_HISTORY_USER_ID",
                table: "CUSTOMER_HISTORY");

            migrationBuilder.DropIndex(
                name: "IX_AUTH_HISTORY_USER_ID",
                table: "AUTH_HISTORY");

            migrationBuilder.DropColumn(
                name: "USER_ID",
                table: "ORDERS");

            migrationBuilder.DropColumn(
                name: "USER_ID",
                table: "NOTIFICATIONS");

            migrationBuilder.DropColumn(
                name: "NEW_VALUE",
                table: "CUSTOMER_HISTORY");

            migrationBuilder.DropColumn(
                name: "USER_ID",
                table: "CUSTOMER_HISTORY");

            migrationBuilder.DropColumn(
                name: "USER_ID",
                table: "AUTH_HISTORY");

            migrationBuilder.RenameColumn(
                name: "MODIFIED_AT",
                table: "CUSTOMER_HISTORY",
                newName: "UPDATED_AT");

            migrationBuilder.AddColumn<string>(
                name: "CUSTOMER_ID",
                table: "ORDERS",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CUSTOMER_ID",
                table: "NOTIFICATIONS",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CUSTOMER_ID",
                table: "CUSTOMER_HISTORY",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CUSTOMER_ID",
                table: "AUTH_HISTORY",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CUSTOMERS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false),
                    FIRST_NAME = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    LAST_NAME = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    REMEMBER_TOKEN = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ACCESS_TOKEN",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CUSTOMER_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false),
                    EXPIRE_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LAST_USED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TOKEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCESS_TOKEN", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ACCESS_TOKEN_CUSTOMERS_CUSTOMER_ID",
                        column: x => x.CUSTOMER_ID,
                        principalTable: "CUSTOMERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EMAIL_VERIFY",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CUSTOMER_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false),
                    IS_VERIFY = table.Column<bool>(type: "bit", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VERIFY_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMAIL_VERIFY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EMAIL_VERIFY_CUSTOMERS_CUSTOMER_ID",
                        column: x => x.CUSTOMER_ID,
                        principalTable: "CUSTOMERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_CUSTOMER_ID",
                table: "ORDERS",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICATIONS_CUSTOMER_ID",
                table: "NOTIFICATIONS",
                column: "CUSTOMER_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMER_HISTORY_CUSTOMER_ID",
                table: "CUSTOMER_HISTORY",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_HISTORY_CUSTOMER_ID",
                table: "AUTH_HISTORY",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ACCESS_TOKEN_CUSTOMER_ID",
                table: "ACCESS_TOKEN",
                column: "CUSTOMER_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMERS_Email_UserName",
                table: "CUSTOMERS",
                columns: new[] { "Email", "UserName" });

            migrationBuilder.CreateIndex(
                name: "IX_EMAIL_VERIFY_CUSTOMER_ID",
                table: "EMAIL_VERIFY",
                column: "CUSTOMER_ID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AUTH_HISTORY_CUSTOMERS_CUSTOMER_ID",
                table: "AUTH_HISTORY",
                column: "CUSTOMER_ID",
                principalTable: "CUSTOMERS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CUSTOMER_HISTORY_CUSTOMERS_CUSTOMER_ID",
                table: "CUSTOMER_HISTORY",
                column: "CUSTOMER_ID",
                principalTable: "CUSTOMERS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NOTIFICATIONS_CUSTOMERS_CUSTOMER_ID",
                table: "NOTIFICATIONS",
                column: "CUSTOMER_ID",
                principalTable: "CUSTOMERS",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ORDERS_CUSTOMERS_CUSTOMER_ID",
                table: "ORDERS",
                column: "CUSTOMER_ID",
                principalTable: "CUSTOMERS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
