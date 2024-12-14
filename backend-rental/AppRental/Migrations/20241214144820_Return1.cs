using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppRental.Migrations
{
    /// <inheritdoc />
    public partial class Return1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReturnDescription",
                table: "Rents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "Rents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkerNote",
                table: "Rents",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnDescription",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "WorkerNote",
                table: "Rents");
        }
    }
}
