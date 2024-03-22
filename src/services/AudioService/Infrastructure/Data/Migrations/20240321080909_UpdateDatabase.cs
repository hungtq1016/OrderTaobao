using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AudioService.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Audios_AudioId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Collections_CollectionId",
                table: "Albums");

            migrationBuilder.AddColumn<string>(
                name: "AudioId1",
                table: "Albums",
                type: "varchar(36)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollectionId1",
                table: "Albums",
                type: "varchar(36)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Albums_AudioId1",
                table: "Albums",
                column: "AudioId1");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_CollectionId1",
                table: "Albums",
                column: "CollectionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Audios_AudioId",
                table: "Albums",
                column: "AudioId",
                principalTable: "Audios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Audios_AudioId1",
                table: "Albums",
                column: "AudioId1",
                principalTable: "Audios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Collections_CollectionId",
                table: "Albums",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Collections_CollectionId1",
                table: "Albums",
                column: "CollectionId1",
                principalTable: "Collections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Audios_AudioId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Audios_AudioId1",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Collections_CollectionId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Collections_CollectionId1",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_AudioId1",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_CollectionId1",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "AudioId1",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "CollectionId1",
                table: "Albums");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Audios_AudioId",
                table: "Albums",
                column: "AudioId",
                principalTable: "Audios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Collections_CollectionId",
                table: "Albums",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id");
        }
    }
}
