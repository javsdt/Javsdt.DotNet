using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Javsdt.Data.Migrations
{
    /// <inheritdoc />
    public partial class tagName错误 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Tags_TagName",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_TagName",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "TagName",
                table: "Cars");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TagName",
                table: "Cars",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_TagName",
                table: "Cars",
                column: "TagName");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Tags_TagName",
                table: "Cars",
                column: "TagName",
                principalTable: "Tags",
                principalColumn: "Name");
        }
    }
}
