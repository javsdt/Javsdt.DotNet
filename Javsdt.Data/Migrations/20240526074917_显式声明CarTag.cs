using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Javsdt.Data.Migrations
{
    /// <inheritdoc />
    public partial class 显式声明CarTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Publishers_PublisherName",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_CarTags_Cars_CarsName",
                table: "CarTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CarTags_Tags_TagsName",
                table: "CarTags");

            migrationBuilder.DropTable(
                name: "CarGenres");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.RenameColumn(
                name: "TagsName",
                table: "CarTags",
                newName: "TagName");

            migrationBuilder.RenameColumn(
                name: "CarsName",
                table: "CarTags",
                newName: "CarName");

            migrationBuilder.RenameIndex(
                name: "IX_CarTags_TagsName",
                table: "CarTags",
                newName: "IX_CarTags_TagName");

            migrationBuilder.RenameColumn(
                name: "PublisherName",
                table: "Cars",
                newName: "TagName");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_PublisherName",
                table: "Cars",
                newName: "IX_Cars_TagName");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Tags_TagName",
                table: "Cars",
                column: "TagName",
                principalTable: "Tags",
                principalColumn: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_CarTags_Cars_CarName",
                table: "CarTags",
                column: "CarName",
                principalTable: "Cars",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarTags_Tags_TagName",
                table: "CarTags",
                column: "TagName",
                principalTable: "Tags",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Tags_TagName",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_CarTags_Cars_CarName",
                table: "CarTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CarTags_Tags_TagName",
                table: "CarTags");

            migrationBuilder.RenameColumn(
                name: "TagName",
                table: "CarTags",
                newName: "TagsName");

            migrationBuilder.RenameColumn(
                name: "CarName",
                table: "CarTags",
                newName: "CarsName");

            migrationBuilder.RenameIndex(
                name: "IX_CarTags_TagName",
                table: "CarTags",
                newName: "IX_CarTags_TagsName");

            migrationBuilder.RenameColumn(
                name: "TagName",
                table: "Cars",
                newName: "PublisherName");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_TagName",
                table: "Cars",
                newName: "IX_Cars_PublisherName");

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BusName = table.Column<string>(type: "TEXT", nullable: true),
                    ChtName = table.Column<string>(type: "TEXT", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DbName = table.Column<string>(type: "TEXT", nullable: true),
                    LibraryName = table.Column<string>(type: "TEXT", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Remark = table.Column<string>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    ZhName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifyTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    ZhName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Name);
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

            migrationBuilder.CreateIndex(
                name: "IX_CarGenres_GenresId",
                table: "CarGenres",
                column: "GenresId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Publishers_PublisherName",
                table: "Cars",
                column: "PublisherName",
                principalTable: "Publishers",
                principalColumn: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_CarTags_Cars_CarsName",
                table: "CarTags",
                column: "CarsName",
                principalTable: "Cars",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarTags_Tags_TagsName",
                table: "CarTags",
                column: "TagsName",
                principalTable: "Tags",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
