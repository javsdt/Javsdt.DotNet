using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Javsdt.Data.Migrations
{
    /// <inheritdoc />
    public partial class jav新增家族序号 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JavSubtitles_Javs_JavsID",
                table: "JavSubtitles");

            migrationBuilder.DropForeignKey(
                name: "FK_JavSubtitles_Subtitles_SubtitlesID",
                table: "JavSubtitles");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Subtitles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "OriginAbsolutePath",
                table: "Subtitles",
                newName: "OriginNameWithoutExt");

            migrationBuilder.RenameColumn(
                name: "SubtitlesID",
                table: "JavSubtitles",
                newName: "SubtitlesId");

            migrationBuilder.RenameColumn(
                name: "JavsID",
                table: "JavSubtitles",
                newName: "JavsId");

            migrationBuilder.RenameIndex(
                name: "IX_JavSubtitles_SubtitlesID",
                table: "JavSubtitles",
                newName: "IX_JavSubtitles_SubtitlesId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Javs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "OriginAbsolutePath",
                table: "Javs",
                newName: "OriginNameWithoutExt");

            migrationBuilder.RenameColumn(
                name: "OnlyEdition",
                table: "Javs",
                newName: "FamilyNo");

            migrationBuilder.AddColumn<string>(
                name: "OriginDir",
                table: "Subtitles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FamilyCount",
                table: "Javs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OriginDir",
                table: "Javs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_JavSubtitles_Javs_JavsId",
                table: "JavSubtitles",
                column: "JavsId",
                principalTable: "Javs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JavSubtitles_Subtitles_SubtitlesId",
                table: "JavSubtitles",
                column: "SubtitlesId",
                principalTable: "Subtitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JavSubtitles_Javs_JavsId",
                table: "JavSubtitles");

            migrationBuilder.DropForeignKey(
                name: "FK_JavSubtitles_Subtitles_SubtitlesId",
                table: "JavSubtitles");

            migrationBuilder.DropColumn(
                name: "OriginDir",
                table: "Subtitles");

            migrationBuilder.DropColumn(
                name: "FamilyCount",
                table: "Javs");

            migrationBuilder.DropColumn(
                name: "OriginDir",
                table: "Javs");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Subtitles",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "OriginNameWithoutExt",
                table: "Subtitles",
                newName: "OriginAbsolutePath");

            migrationBuilder.RenameColumn(
                name: "SubtitlesId",
                table: "JavSubtitles",
                newName: "SubtitlesID");

            migrationBuilder.RenameColumn(
                name: "JavsId",
                table: "JavSubtitles",
                newName: "JavsID");

            migrationBuilder.RenameIndex(
                name: "IX_JavSubtitles_SubtitlesId",
                table: "JavSubtitles",
                newName: "IX_JavSubtitles_SubtitlesID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Javs",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "OriginNameWithoutExt",
                table: "Javs",
                newName: "OriginAbsolutePath");

            migrationBuilder.RenameColumn(
                name: "FamilyNo",
                table: "Javs",
                newName: "OnlyEdition");

            migrationBuilder.AddForeignKey(
                name: "FK_JavSubtitles_Javs_JavsID",
                table: "JavSubtitles",
                column: "JavsID",
                principalTable: "Javs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JavSubtitles_Subtitles_SubtitlesID",
                table: "JavSubtitles",
                column: "SubtitlesID",
                principalTable: "Subtitles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
