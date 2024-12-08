using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppRental.Migrations
{
    /// <inheritdoc />
    public partial class AddRentStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmed",
                table: "Rents");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Rents",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Rents");

            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                table: "Rents",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
