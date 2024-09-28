using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Javsdt.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarsPrefs",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxSuf = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarsPrefs", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Crews",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ZhName = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crews", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DbName = table.Column<string>(type: "TEXT", nullable: true),
                    LibraryName = table.Column<string>(type: "TEXT", nullable: true),
                    BusName = table.Column<string>(type: "TEXT", nullable: true),
                    ZhName = table.Column<string>(type: "TEXT", nullable: true),
                    ChtName = table.Column<string>(type: "TEXT", nullable: true),
                    Remark = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Javs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CarName = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSeparate = table.Column<bool>(type: "INTEGER", nullable: false),
                    Edition = table.Column<string>(type: "TEXT", nullable: true),
                    OnlyEdition = table.Column<bool>(type: "INTEGER", nullable: false),
                    CD = table.Column<int>(type: "INTEGER", nullable: false),
                    CDCount = table.Column<int>(type: "INTEGER", nullable: false),
                    HasSubtitle = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDivulged = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsCracked = table.Column<bool>(type: "INTEGER", nullable: false),
                    FanartPath = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    Error = table.Column<int>(type: "INTEGER", nullable: false),
                    OriginAbsolutePath = table.Column<string>(type: "TEXT", nullable: false),
                    Dir = table.Column<string>(type: "TEXT", nullable: false),
                    NameWithoutExt = table.Column<string>(type: "TEXT", nullable: false),
                    Ext = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Javs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NasJavFolders",
                columns: table => new
                {
                    Path = table.Column<string>(type: "TEXT", nullable: false),
                    CarName = table.Column<string>(type: "TEXT", nullable: false),
                    HasVideo = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NasJavFolders", x => x.Path);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ZhName = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Seriess",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ZhName = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seriess", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Studios",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ZhName = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studios", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Subtitles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CarName = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    Error = table.Column<int>(type: "INTEGER", nullable: false),
                    OriginAbsolutePath = table.Column<string>(type: "TEXT", nullable: false),
                    Dir = table.Column<string>(type: "TEXT", nullable: false),
                    NameWithoutExt = table.Column<string>(type: "TEXT", nullable: false),
                    Ext = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subtitles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    TName = table.Column<string>(type: "TEXT", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    OriginName = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    ZhTitle = table.Column<string>(type: "TEXT", nullable: true),
                    Plot = table.Column<string>(type: "TEXT", nullable: true),
                    ZhPlot = table.Column<string>(type: "TEXT", nullable: true),
                    Review = table.Column<string>(type: "TEXT", nullable: true),
                    Release = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Runtime = table.Column<int>(type: "INTEGER", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false),
                    JavLibrary = table.Column<string>(type: "TEXT", nullable: true),
                    JavDb = table.Column<string>(type: "TEXT", nullable: true),
                    JavBus = table.Column<string>(type: "TEXT", nullable: true),
                    Jav321 = table.Column<string>(type: "TEXT", nullable: true),
                    Arzon = table.Column<string>(type: "TEXT", nullable: true),
                    Only = table.Column<bool>(type: "INTEGER", nullable: false),
                    DbCover = table.Column<string>(type: "TEXT", nullable: true),
                    LibraryCover = table.Column<string>(type: "TEXT", nullable: true),
                    BusCover = table.Column<string>(type: "TEXT", nullable: true),
                    DmmCover = table.Column<string>(type: "TEXT", nullable: true),
                    T21Cover = table.Column<string>(type: "TEXT", nullable: true),
                    Poster = table.Column<string>(type: "TEXT", nullable: true),
                    Crop = table.Column<int>(type: "INTEGER", nullable: false),
                    CarPrefName = table.Column<string>(type: "TEXT", nullable: true),
                    SeriesName = table.Column<string>(type: "TEXT", nullable: true),
                    PublisherName = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Cars_CarsPrefs_CarPrefName",
                        column: x => x.CarPrefName,
                        principalTable: "CarsPrefs",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_Cars_Publishers_PublisherName",
                        column: x => x.PublisherName,
                        principalTable: "Publishers",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_Cars_Seriess_SeriesName",
                        column: x => x.SeriesName,
                        principalTable: "Seriess",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "JavSubtitles",
                columns: table => new
                {
                    JavsID = table.Column<int>(type: "INTEGER", nullable: false),
                    SubtitlesID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JavSubtitles", x => new { x.JavsID, x.SubtitlesID });
                    table.ForeignKey(
                        name: "FK_JavSubtitles_Javs_JavsID",
                        column: x => x.JavsID,
                        principalTable: "Javs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JavSubtitles_Subtitles_SubtitlesID",
                        column: x => x.SubtitlesID,
                        principalTable: "Subtitles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebTags",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Website = table.Column<int>(type: "INTEGER", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", nullable: true),
                    TagName = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebTags", x => new { x.Name, x.Website });
                    table.ForeignKey(
                        name: "FK_WebTags_Tags_TagName",
                        column: x => x.TagName,
                        principalTable: "Tags",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "CarCrews",
                columns: table => new
                {
                    CarName = table.Column<string>(type: "TEXT", nullable: false),
                    CrewName = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCrews", x => new { x.CarName, x.CrewName, x.Type });
                    table.ForeignKey(
                        name: "FK_CarCrews_Cars_CarName",
                        column: x => x.CarName,
                        principalTable: "Cars",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarCrews_Crews_CrewName",
                        column: x => x.CrewName,
                        principalTable: "Crews",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarGenres",
                columns: table => new
                {
                    CarsName = table.Column<string>(type: "TEXT", nullable: false),
                    GenresId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarGenres", x => new { x.CarsName, x.GenresId });
                    table.ForeignKey(
                        name: "FK_CarGenres_Cars_CarsName",
                        column: x => x.CarsName,
                        principalTable: "Cars",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarGenres_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarStudios",
                columns: table => new
                {
                    CarName = table.Column<string>(type: "TEXT", nullable: false),
                    StudioName = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarStudios", x => new { x.CarName, x.StudioName, x.Type });
                    table.ForeignKey(
                        name: "FK_CarStudios_Cars_CarName",
                        column: x => x.CarName,
                        principalTable: "Cars",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarStudios_Studios_StudioName",
                        column: x => x.StudioName,
                        principalTable: "Studios",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarTags",
                columns: table => new
                {
                    CarsName = table.Column<string>(type: "TEXT", nullable: false),
                    TagsName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTags", x => new { x.CarsName, x.TagsName });
                    table.ForeignKey(
                        name: "FK_CarTags_Cars_CarsName",
                        column: x => x.CarsName,
                        principalTable: "Cars",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarTags_Tags_TagsName",
                        column: x => x.TagsName,
                        principalTable: "Tags",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarCrews_CrewName",
                table: "CarCrews",
                column: "CrewName");

            migrationBuilder.CreateIndex(
                name: "IX_CarGenres_GenresId",
                table: "CarGenres",
                column: "GenresId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarPrefName",
                table: "Cars",
                column: "CarPrefName");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_PublisherName",
                table: "Cars",
                column: "PublisherName");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_SeriesName",
                table: "Cars",
                column: "SeriesName");

            migrationBuilder.CreateIndex(
                name: "IX_CarStudios_StudioName",
                table: "CarStudios",
                column: "StudioName");

            migrationBuilder.CreateIndex(
                name: "IX_CarTags_TagsName",
                table: "CarTags",
                column: "TagsName");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_BusName",
                table: "Genres",
                column: "BusName");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_DbName",
                table: "Genres",
                column: "DbName");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_LibraryName",
                table: "Genres",
                column: "LibraryName");

            migrationBuilder.CreateIndex(
                name: "IX_JavSubtitles_SubtitlesID",
                table: "JavSubtitles",
                column: "SubtitlesID");

            migrationBuilder.CreateIndex(
                name: "IX_WebTags_TagName",
                table: "WebTags",
                column: "TagName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarCrews");

            migrationBuilder.DropTable(
                name: "CarGenres");

            migrationBuilder.DropTable(
                name: "CarStudios");

            migrationBuilder.DropTable(
                name: "CarTags");

            migrationBuilder.DropTable(
                name: "JavSubtitles");

            migrationBuilder.DropTable(
                name: "NasJavFolders");

            migrationBuilder.DropTable(
                name: "WebTags");

            migrationBuilder.DropTable(
                name: "Crews");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Studios");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Javs");

            migrationBuilder.DropTable(
                name: "Subtitles");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "CarsPrefs");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Seriess");
        }
    }
}
