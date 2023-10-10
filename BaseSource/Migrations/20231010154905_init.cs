using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseSource.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CATEGORIES",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SLUG = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PARENT_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false)
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
                name: "CUSTOMERS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    REMEMBER_TOKEN = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FIRST_NAME = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    LAST_NAME = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    CATEGORY_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false)
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
                name: "ACCESS_TOKEN",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    TOKEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LAST_USED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EXPIRE_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CUSTOMER_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false)
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
                    CONTENT = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CUSTOMER_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false)
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
                name: "EMAIL_VERIFY",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    VERIFY_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IS_VERIFY = table.Column<bool>(type: "bit", nullable: false),
                    CUSTOMER_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false)
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
                    CUSTOMER_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false)
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
                    UPDATED_BY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_ACCESS_TOKEN_CUSTOMER_ID",
                table: "ACCESS_TOKEN",
                column: "CUSTOMER_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "IX_CUSTOMERS_Email_UserName",
                table: "CUSTOMERS",
                columns: new[] { "Email", "UserName" });

            migrationBuilder.CreateIndex(
                name: "IX_EMAIL_VERIFY_CUSTOMER_ID",
                table: "EMAIL_VERIFY",
                column: "CUSTOMER_ID",
                unique: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACCESS_TOKEN");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AUTH_HISTORY");

            migrationBuilder.DropTable(
                name: "CUSTOMER_HISTORY");

            migrationBuilder.DropTable(
                name: "EMAIL_VERIFY");

            migrationBuilder.DropTable(
                name: "NOTIFICATIONS");

            migrationBuilder.DropTable(
                name: "ORDER_DETAILS");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ORDERS");

            migrationBuilder.DropTable(
                name: "PRODUCTS");

            migrationBuilder.DropTable(
                name: "CUSTOMERS");

            migrationBuilder.DropTable(
                name: "CATEGORIES");
        }
    }
}
