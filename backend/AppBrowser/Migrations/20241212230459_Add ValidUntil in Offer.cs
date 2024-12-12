using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppBrowser.Migrations
{
    /// <inheritdoc />
    public partial class AddValidUntilinOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ValidUntil",
                table: "Offers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidUntil",
                table: "Offers");
        }
    }
}
