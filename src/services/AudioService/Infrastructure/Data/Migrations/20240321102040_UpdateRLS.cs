using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AudioService.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRLS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Collections_CollectionId1",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_CollectionId1",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "CollectionId1",
                table: "Albums");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CollectionId1",
                table: "Albums",
                type: "varchar(36)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Albums_CollectionId1",
                table: "Albums",
                column: "CollectionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Collections_CollectionId1",
                table: "Albums",
                column: "CollectionId1",
                principalTable: "Collections",
                principalColumn: "Id");
        }
    }
}
