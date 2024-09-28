using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Javsdt.Data.Migrations
{
    /// <inheritdoc />
    public partial class CarPref新增jav类型 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UpdateStatus",
                table: "CarsPrefs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateStatus",
                table: "CarsPrefs");
        }
    }
}
