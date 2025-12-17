using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Javsdt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class error改名为status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Error",
                table: "Subtitles",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "CarName",
                table: "Subtitles",
                newName: "Car");

            migrationBuilder.RenameColumn(
                name: "Error",
                table: "Javs",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Subtitles",
                newName: "Error");

            migrationBuilder.RenameColumn(
                name: "Car",
                table: "Subtitles",
                newName: "CarName");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Javs",
                newName: "Error");
        }
    }
}
