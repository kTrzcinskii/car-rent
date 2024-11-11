using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppRental.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cost",
                table: "Cars",
                newName: "BaseCostPerDay");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BaseCostPerDay",
                table: "Cars",
                newName: "Cost");
        }
    }
}
