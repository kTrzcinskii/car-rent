using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppRental.Migrations
{
    /// <inheritdoc />
    public partial class ChangedRent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnDescription",
                table: "Rents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReturnDescription",
                table: "Rents",
                type: "text",
                nullable: true);
        }
    }
}
