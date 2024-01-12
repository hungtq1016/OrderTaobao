using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaseSource.Migrations
{
    /// <inheritdoc />
    public partial class init_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdministrativeUnit",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    SLUG = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministrativeUnit", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CATEGORIES",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SLUG = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IMAGES",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    LABEL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TYPE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SIZE = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMAGES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SLUG = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PRICE = table.Column<long>(type: "bigint", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ROLE",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_USER", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PROVINCES",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SLUG = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NAME_EN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EN_SLUG = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CODE = table.Column<int>(type: "int", nullable: false),
                    ADMINISTRATIVE_UNIT_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROVINCES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PROVINCES_AdministrativeUnit_ADMINISTRATIVE_UNIT_ID",
                        column: x => x.ADMINISTRATIVE_UNIT_ID,
                        principalTable: "AdministrativeUnit",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_CATEGORY",
                columns: table => new
                {
                    PRODUCT_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CATEGORY_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT_CATEGORY", x => new { x.PRODUCT_ID, x.CATEGORY_ID });
                    table.ForeignKey(
                        name: "FK_PRODUCT_CATEGORY_CATEGORIES_CATEGORY_ID",
                        column: x => x.CATEGORY_ID,
                        principalTable: "CATEGORIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRODUCT_CATEGORY_PRODUCTS_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ROLE_CLAIM",
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
                    table.PrimaryKey("PK_ROLE_CLAIM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ROLE_CLAIM_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ROLE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ROLE",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AUDIT_TRAIL",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    USER_ID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    TABLE_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DEVICE_IP = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ACTION = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    ENTITY_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    STATUS_CODE = table.Column<int>(type: "int", nullable: true),
                    VALUE = table.Column<string>(type: "text", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUDIT_TRAIL", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AUDIT_TRAIL_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USER",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AUTH_HISTORY",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CONTENT = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    USER_ID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_HISTORY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AUTH_HISTORY_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USER",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMER_HISTORY",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    OLD_VALUE = table.Column<string>(type: "nvarchar", nullable: false),
                    NEW_VALUE = table.Column<string>(type: "nvarchar", nullable: false),
                    FIELD = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    MODIFIED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    USER_ID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMER_HISTORY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CUSTOMER_HISTORY_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IMAGE_USER",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    USER_ID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    IMAGE_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMAGE_USER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IMAGE_USER_IMAGES_IMAGE_ID",
                        column: x => x.IMAGE_ID,
                        principalTable: "IMAGES",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_IMAGE_USER_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USER",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ORDERS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    STATUS = table.Column<byte>(type: "tinyint", nullable: false),
                    USER_ID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDERS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ORDERS_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USER",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RESET_PASSWORD",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IS_VERIFY = table.Column<bool>(type: "bit", nullable: false),
                    ExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "USER_CLAIM",
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
                    table.PrimaryKey("PK_USER_CLAIM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USER_CLAIM_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_LOGIN",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_LOGIN", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_USER_LOGIN_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_TOKEN",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_TOKEN", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_USER_TOKEN_USER_UserId",
                        column: x => x.UserId,
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
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SLUG = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NAME_EN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EN_SLUG = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CODE = table.Column<int>(type: "int", nullable: false),
                    ADMINISTRATIVE_UNIT_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DISTRICTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DISTRICTS_AdministrativeUnit_ADMINISTRATIVE_UNIT_ID",
                        column: x => x.ADMINISTRATIVE_UNIT_ID,
                        principalTable: "AdministrativeUnit",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_DISTRICTS_PROVINCES_PROVINCE_ID",
                        column: x => x.PROVINCE_ID,
                        principalTable: "PROVINCES",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "NOTIFICATIONS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CONTENT = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IS_READ = table.Column<bool>(type: "bit", nullable: false),
                    USER_ID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ORDER_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTIFICATIONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NOTIFICATIONS_ORDERS_ORDER_ID",
                        column: x => x.ORDER_ID,
                        principalTable: "ORDERS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_NOTIFICATIONS_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USER",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ORDER_DETAILS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false),
                    PRICE = table.Column<long>(type: "bigint", nullable: false),
                    PRODUCT_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    ORDER_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "WARDS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    DISTRICT_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SLUG = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NAME_EN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EN_SLUG = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CODE = table.Column<int>(type: "int", nullable: false),
                    ADMINISTRATIVE_UNIT_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WARDS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WARDS_AdministrativeUnit_ADMINISTRATIVE_UNIT_ID",
                        column: x => x.ADMINISTRATIVE_UNIT_ID,
                        principalTable: "AdministrativeUnit",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_WARDS_DISTRICTS_DISTRICT_ID",
                        column: x => x.DISTRICT_ID,
                        principalTable: "DISTRICTS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ADDRESS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    STREET = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    WARD_ID = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    USER_ID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENABLE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADDRESS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ADDRESS_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USER",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ADDRESS_WARDS_WARD_ID",
                        column: x => x.WARD_ID,
                        principalTable: "WARDS",
                        principalColumn: "ID");
                });

            migrationBuilder.InsertData(
                table: "ROLE",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00216558-b156-497f-ae76-dee369eb93c0", null, "Admin", "ADMIN" },
                    { "14ac5cb7-c7ce-4a74-8e8e-78e0324e4e64", null, "Staff", "STAFF" },
                    { "1f3c1817-ba71-4277-ac92-c4676a6facc0", null, "Super Admin", "SUPER ADMIN" },
                    { "59bad76c-4fad-4279-9ec5-de521378602e", null, "Manager", "MANAGER" },
                    { "63d5f700-0e02-4f6b-8760-406196cfb2e5", null, "Visitor", "VISITOR" },
                    { "7d4d449e-da9d-4015-8388-837797511dec", null, "Customer", "CUSTOMER" },
                    { "ac294ece-68cc-4ea1-bde9-6ab257d14f0b", null, "Collaborator", "COLLABORATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ADDRESS_USER_ID",
                table: "ADDRESS",
                column: "USER_ID",
                unique: true,
                filter: "[USER_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ADDRESS_WARD_ID",
                table: "ADDRESS",
                column: "WARD_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AUDIT_TRAIL_USER_ID",
                table: "AUDIT_TRAIL",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AUTH_HISTORY_USER_ID",
                table: "AUTH_HISTORY",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMER_HISTORY_USER_ID",
                table: "CUSTOMER_HISTORY",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DISTRICTS_ADMINISTRATIVE_UNIT_ID",
                table: "DISTRICTS",
                column: "ADMINISTRATIVE_UNIT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DISTRICTS_PROVINCE_ID",
                table: "DISTRICTS",
                column: "PROVINCE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_IMAGE_USER_IMAGE_ID",
                table: "IMAGE_USER",
                column: "IMAGE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_IMAGE_USER_USER_ID",
                table: "IMAGE_USER",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICATIONS_ORDER_ID",
                table: "NOTIFICATIONS",
                column: "ORDER_ID",
                unique: true,
                filter: "[ORDER_ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICATIONS_USER_ID",
                table: "NOTIFICATIONS",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_DETAILS_ORDER_ID",
                table: "ORDER_DETAILS",
                column: "ORDER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_DETAILS_PRODUCT_ID",
                table: "ORDER_DETAILS",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_USER_ID",
                table: "ORDERS",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_CATEGORY_CATEGORY_ID",
                table: "PRODUCT_CATEGORY",
                column: "CATEGORY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PROVINCES_ADMINISTRATIVE_UNIT_ID",
                table: "PROVINCES",
                column: "ADMINISTRATIVE_UNIT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RESET_PASSWORD_EMAIL",
                table: "RESET_PASSWORD",
                column: "EMAIL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "ROLE",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ROLE_CLAIM_RoleId",
                table: "ROLE_CLAIM",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "USER",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "USER",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_USER_CLAIM_UserId",
                table: "USER_CLAIM",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_LOGIN_UserId",
                table: "USER_LOGIN",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WARDS_ADMINISTRATIVE_UNIT_ID",
                table: "WARDS",
                column: "ADMINISTRATIVE_UNIT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_WARDS_DISTRICT_ID",
                table: "WARDS",
                column: "DISTRICT_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADDRESS");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AUDIT_TRAIL");

            migrationBuilder.DropTable(
                name: "AUTH_HISTORY");

            migrationBuilder.DropTable(
                name: "CUSTOMER_HISTORY");

            migrationBuilder.DropTable(
                name: "IMAGE_USER");

            migrationBuilder.DropTable(
                name: "NOTIFICATIONS");

            migrationBuilder.DropTable(
                name: "ORDER_DETAILS");

            migrationBuilder.DropTable(
                name: "PRODUCT_CATEGORY");

            migrationBuilder.DropTable(
                name: "RESET_PASSWORD");

            migrationBuilder.DropTable(
                name: "ROLE_CLAIM");

            migrationBuilder.DropTable(
                name: "USER_CLAIM");

            migrationBuilder.DropTable(
                name: "USER_LOGIN");

            migrationBuilder.DropTable(
                name: "USER_TOKEN");

            migrationBuilder.DropTable(
                name: "WARDS");

            migrationBuilder.DropTable(
                name: "IMAGES");

            migrationBuilder.DropTable(
                name: "ORDERS");

            migrationBuilder.DropTable(
                name: "CATEGORIES");

            migrationBuilder.DropTable(
                name: "PRODUCTS");

            migrationBuilder.DropTable(
                name: "ROLE");

            migrationBuilder.DropTable(
                name: "DISTRICTS");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "PROVINCES");

            migrationBuilder.DropTable(
                name: "AdministrativeUnit");
        }
    }
}
