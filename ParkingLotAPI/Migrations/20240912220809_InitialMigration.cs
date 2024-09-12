using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingLotAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fares", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicensePlate = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parkings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FareId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    EntryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExitTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parkings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parkings_Fares_FareId",
                        column: x => x.FareId,
                        principalTable: "Fares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parkings_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parkings_FareId",
                table: "Parkings",
                column: "FareId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parkings_VehicleId",
                table: "Parkings",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_LicensePlate",
                table: "Vehicles",
                column: "LicensePlate",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parkings");

            migrationBuilder.DropTable(
                name: "Fares");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
