using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaseSource.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORIES",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SLUG = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false),
                    PARENT_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CATEGORIES_CATEGORIES_PARENT_ID",
                        column: x => x.PARENT_ID,
                        principalTable: "CATEGORIES",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PROVINCES",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "ROLES",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SLUG = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PRICE = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false),
                    IS_AVAILABLE = table.Column<bool>(type: "bit", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false),
                    CATEGORY_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUCTS_CATEGORIES_CATEGORY_ID",
                        column: x => x.CATEGORY_ID,
                        principalTable: "CATEGORIES",
                        principalColumn: "ID",
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
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ADMIN",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false),
                    USER_NAME = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FIRST_NAME = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    LAST_NAME = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PHONE = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    ADDRESS_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMIN", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ADMIN_ADDRESS_ADDRESS_ID",
                        column: x => x.ADDRESS_ID,
                        principalTable: "ADDRESS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMERS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    REMEMBER_TOKEN = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false),
                    USER_NAME = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FIRST_NAME = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    LAST_NAME = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PHONE = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    ADDRESS_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMERS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CUSTOMERS_ADDRESS_ADDRESS_ID",
                        column: x => x.ADDRESS_ID,
                        principalTable: "ADDRESS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ACCESS_TOKEN",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    TOKEN = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    LAST_USED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EXPIRE_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsVerify = table.Column<bool>(type: "bit", nullable: false),
                    CUSTOMER_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "AUTH_HISTORY",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CONTENT = table.Column<string>(type: "nvarchar", nullable: false),
                    CUSTOMER_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_HISTORY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AUTH_HISTORY_CUSTOMERS_CUSTOMER_ID",
                        column: x => x.CUSTOMER_ID,
                        principalTable: "CUSTOMERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMER_HISTORY",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    OLD_VALUE = table.Column<string>(type: "nvarchar", nullable: false),
                    FIELD = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CUSTOMER_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMER_HISTORY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CUSTOMER_HISTORY_CUSTOMERS_CUSTOMER_ID",
                        column: x => x.CUSTOMER_ID,
                        principalTable: "CUSTOMERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMER_ROLE",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CUSTOMER_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    ROLE_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMER_ROLE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CUSTOMER_ROLE_CUSTOMERS_CUSTOMER_ID",
                        column: x => x.CUSTOMER_ID,
                        principalTable: "CUSTOMERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CUSTOMER_ROLE_ROLES_ROLE_ID",
                        column: x => x.ROLE_ID,
                        principalTable: "ROLES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EMAIL_VERIFY",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    VERIFY_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IS_VERIFY = table.Column<bool>(type: "bit", nullable: false),
                    CUSTOMER_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ORDERS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    STATUS = table.Column<int>(type: "int", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false),
                    CUSTOMER_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDERS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ORDERS_CUSTOMERS_CUSTOMER_ID",
                        column: x => x.CUSTOMER_ID,
                        principalTable: "CUSTOMERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NOTIFICATIONS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CONTENT = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IS_READ = table.Column<bool>(type: "bit", nullable: false),
                    CUSTOMER_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    ORDER_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTIFICATIONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NOTIFICATIONS_CUSTOMERS_CUSTOMER_ID",
                        column: x => x.CUSTOMER_ID,
                        principalTable: "CUSTOMERS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_NOTIFICATIONS_ORDERS_ORDER_ID",
                        column: x => x.ORDER_ID,
                        principalTable: "ORDERS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ORDER_DETAILS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false),
                    PRICE = table.Column<int>(type: "int", nullable: false),
                    PRODUCT_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    ORDER_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER_DETAILS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ORDER_DETAILS_ORDERS_ORDER_ID",
                        column: x => x.ORDER_ID,
                        principalTable: "ORDERS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ORDER_DETAILS_PRODUCTS_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCTS",
                        principalColumn: "ID");
                });

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
                name: "IX_ACCESS_TOKEN_CUSTOMER_ID",
                table: "ACCESS_TOKEN",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ADDRESS_WARD_ID",
                table: "ADDRESS",
                column: "WARD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ADMIN_ADDRESS_ID",
                table: "ADMIN",
                column: "ADDRESS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_HISTORY_CUSTOMER_ID",
                table: "AUTH_HISTORY",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORIES_PARENT_ID",
                table: "CATEGORIES",
                column: "PARENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMER_HISTORY_CUSTOMER_ID",
                table: "CUSTOMER_HISTORY",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMER_ROLE_CUSTOMER_ID",
                table: "CUSTOMER_ROLE",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMER_ROLE_ROLE_ID",
                table: "CUSTOMER_ROLE",
                column: "ROLE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMERS_ADDRESS_ID",
                table: "CUSTOMERS",
                column: "ADDRESS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMERS_EMAIL_USER_NAME",
                table: "CUSTOMERS",
                columns: new[] { "EMAIL", "USER_NAME" });

            migrationBuilder.CreateIndex(
                name: "IX_DISTRICTS_PROVINCE_ID",
                table: "DISTRICTS",
                column: "PROVINCE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_EMAIL_VERIFY_CUSTOMER_ID",
                table: "EMAIL_VERIFY",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICATIONS_CUSTOMER_ID",
                table: "NOTIFICATIONS",
                column: "CUSTOMER_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICATIONS_ORDER_ID",
                table: "NOTIFICATIONS",
                column: "ORDER_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_DETAILS_ORDER_ID",
                table: "ORDER_DETAILS",
                column: "ORDER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_DETAILS_PRODUCT_ID",
                table: "ORDER_DETAILS",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_CUSTOMER_ID",
                table: "ORDERS",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_CATEGORY_ID",
                table: "PRODUCTS",
                column: "CATEGORY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_WARDS_DISTRICT_ID",
                table: "WARDS",
                column: "DISTRICT_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACCESS_TOKEN");

            migrationBuilder.DropTable(
                name: "ADMIN");

            migrationBuilder.DropTable(
                name: "AUTH_HISTORY");

            migrationBuilder.DropTable(
                name: "CUSTOMER_HISTORY");

            migrationBuilder.DropTable(
                name: "CUSTOMER_ROLE");

            migrationBuilder.DropTable(
                name: "EMAIL_VERIFY");

            migrationBuilder.DropTable(
                name: "NOTIFICATIONS");

            migrationBuilder.DropTable(
                name: "ORDER_DETAILS");

            migrationBuilder.DropTable(
                name: "ROLES");

            migrationBuilder.DropTable(
                name: "ORDERS");

            migrationBuilder.DropTable(
                name: "PRODUCTS");

            migrationBuilder.DropTable(
                name: "CUSTOMERS");

            migrationBuilder.DropTable(
                name: "CATEGORIES");

            migrationBuilder.DropTable(
                name: "ADDRESS");

            migrationBuilder.DropTable(
                name: "WARDS");

            migrationBuilder.DropTable(
                name: "DISTRICTS");

            migrationBuilder.DropTable(
                name: "PROVINCES");
        }
    }
}
