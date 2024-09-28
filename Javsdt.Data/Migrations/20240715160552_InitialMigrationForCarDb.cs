using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Javsdt.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationForCarDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JavSubtitles");

            migrationBuilder.DropTable(
                name: "NasJavFolders");

            migrationBuilder.DropTable(
                name: "Javs");

            migrationBuilder.DropTable(
                name: "Subtitles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Javs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CD = table.Column<int>(type: "INTEGER", nullable: false),
                    CDCount = table.Column<int>(type: "INTEGER", nullable: false),
                    CarName = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Dir = table.Column<string>(type: "TEXT", nullable: false),
                    Edition = table.Column<string>(type: "TEXT", nullable: true),
                    Error = table.Column<int>(type: "INTEGER", nullable: false),
                    Ext = table.Column<string>(type: "TEXT", nullable: false),
                    FamilyCount = table.Column<int>(type: "INTEGER", nullable: false),
                    FamilyNo = table.Column<int>(type: "INTEGER", nullable: false),
                    FanartPath = table.Column<string>(type: "TEXT", nullable: true),
                    HasSubtitle = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsCracked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDivulged = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSeparate = table.Column<bool>(type: "INTEGER", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    NameWithoutExt = table.Column<string>(type: "TEXT", nullable: false),
                    OriginDir = table.Column<string>(type: "TEXT", nullable: false),
                    OriginNameWithoutExt = table.Column<string>(type: "TEXT", nullable: false),
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Javs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NasJavFolders",
                columns: table => new
                {
                    Path = table.Column<string>(type: "TEXT", nullable: false),
                    CarName = table.Column<string>(type: "TEXT", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    HasVideo = table.Column<bool>(type: "INTEGER", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NasJavFolders", x => x.Path);
                });

            migrationBuilder.CreateTable(
                name: "Subtitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CarName = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Dir = table.Column<string>(type: "TEXT", nullable: false),
                    Error = table.Column<int>(type: "INTEGER", nullable: false),
                    Ext = table.Column<string>(type: "TEXT", nullable: false),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    NameWithoutExt = table.Column<string>(type: "TEXT", nullable: false),
                    OriginDir = table.Column<string>(type: "TEXT", nullable: false),
                    OriginNameWithoutExt = table.Column<string>(type: "TEXT", nullable: false),
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subtitles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JavSubtitles",
                columns: table => new
                {
                    JavsId = table.Column<int>(type: "INTEGER", nullable: false),
                    SubtitlesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JavSubtitles", x => new { x.JavsId, x.SubtitlesId });
                    table.ForeignKey(
                        name: "FK_JavSubtitles_Javs_JavsId",
                        column: x => x.JavsId,
                        principalTable: "Javs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JavSubtitles_Subtitles_SubtitlesId",
                        column: x => x.SubtitlesId,
                        principalTable: "Subtitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JavSubtitles_SubtitlesId",
                table: "JavSubtitles",
                column: "SubtitlesId");
        }
    }
}
