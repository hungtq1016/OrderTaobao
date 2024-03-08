using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OAuth2Service.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateotp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "$2a$11$k/5Vg6TgEmTfS.27ch.Mp.UrkMr07a6jC7emIO0ECGFdlYnsdL51.",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "$2a$11$WXB7CzGrnqwEqkJEEGMoBez0G5BjTXlkW7kR4LH2uV1c8MAON4pA2");

            migrationBuilder.CreateTable(
                name: "Otps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Otps", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Otps");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "$2a$11$WXB7CzGrnqwEqkJEEGMoBez0G5BjTXlkW7kR4LH2uV1c8MAON4pA2",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "$2a$11$k/5Vg6TgEmTfS.27ch.Mp.UrkMr07a6jC7emIO0ECGFdlYnsdL51.");
        }
    }
}
