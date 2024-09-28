using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Javsdt.Data.Migrations
{
    /// <inheritdoc />
    public partial class 新增ArzonCover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArzonCover",
                table: "Cars",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArzonCover",
                table: "Cars");
        }
    }
}
