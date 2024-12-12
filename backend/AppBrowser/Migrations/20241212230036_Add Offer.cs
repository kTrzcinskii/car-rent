using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AppBrowser.Migrations
{
    /// <inheritdoc />
    public partial class AddOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Cars_CarId",
                table: "Rents");

            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Users_UserId",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "CostPerDay",
                table: "Rents");

            migrationBuilder.DropColumn(
                name: "InsuranceCostPerDay",
                table: "Rents");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "Rents",
                newName: "OfferId");

            migrationBuilder.RenameIndex(
                name: "IX_Rents_CarId",
                table: "Rents",
                newName: "IX_Rents_OfferId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Rents",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    OfferId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProviderId = table.Column<int>(type: "integer", nullable: false),
                    ExternalOfferId = table.Column<int>(type: "integer", nullable: false),
                    CostPerDay = table.Column<decimal>(type: "numeric", nullable: false),
                    InsuranceCostPerDay = table.Column<decimal>(type: "numeric", nullable: false),
                    CarId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.OfferId);
                    table.ForeignKey(
                        name: "FK_Offers_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CarId",
                table: "Offers",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_UserId",
                table: "Offers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Offers_OfferId",
                table: "Rents",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "OfferId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Users_UserId",
                table: "Rents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Offers_OfferId",
                table: "Rents");

            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Users_UserId",
                table: "Rents");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.RenameColumn(
                name: "OfferId",
                table: "Rents",
                newName: "CarId");

            migrationBuilder.RenameIndex(
                name: "IX_Rents_OfferId",
                table: "Rents",
                newName: "IX_Rents_CarId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Rents",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CostPerDay",
                table: "Rents",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InsuranceCostPerDay",
                table: "Rents",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Cars_CarId",
                table: "Rents",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Users_UserId",
                table: "Rents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
