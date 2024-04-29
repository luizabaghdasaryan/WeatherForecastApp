using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TerrainType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Summary",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Summary", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DailyForecast",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    MinTemperature = table.Column<int>(type: "int", nullable: false),
                    MaxTemperature = table.Column<int>(type: "int", nullable: false),
                    SummaryId = table.Column<int>(type: "int", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyForecast", x => x.id);
                    table.ForeignKey(
                        name: "FK_DailyForecast_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyForecast_Summary_SummaryId",
                        column: x => x.SummaryId,
                        principalTable: "Summary",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HourlyForecast",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ForecastId = table.Column<int>(type: "int", nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    TemperatureC = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HourlyForecast", x => x.id);
                    table.ForeignKey(
                        name: "FK_HourlyForecast_DailyForecast_ForecastId",
                        column: x => x.ForecastId,
                        principalTable: "DailyForecast",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Region",
                columns: new[] { "id", "Name", "TerrainType" },
                values: new object[,]
                {
                    { 1, "Shirak", null },
                    { 2, "Kotayk", 0 },
                    { 3, "Kotayk", 1 },
                    { 4, "Gegharkunik", null },
                    { 5, "Lori", null },
                    { 6, "Tavush", null },
                    { 7, "Aragatsotn", 0 },
                    { 8, "Aragatsotn", 1 },
                    { 9, "Ararat", null },
                    { 10, "Armavir", null },
                    { 11, "Vayots Dzor", 0 },
                    { 12, "Vayots Dzor", 1 },
                    { 13, "Syunik", 2 },
                    { 14, "Syunik", 1 }
                });

            migrationBuilder.InsertData(
                table: "Summary",
                columns: new[] { "id", "Name" },
                values: new object[,]
                {
                    { 1, "Freezing" },
                    { 2, "Bracing" },
                    { 3, "Chilly" },
                    { 4, "Cool" },
                    { 5, "Mild" },
                    { 6, "Warm" },
                    { 7, "Balmy" },
                    { 8, "Hot" },
                    { 9, "Sweltering" },
                    { 10, "Scorching" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyForecast_Date_RegionId",
                table: "DailyForecast",
                columns: new[] { "Date", "RegionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyForecast_RegionId",
                table: "DailyForecast",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyForecast_SummaryId",
                table: "DailyForecast",
                column: "SummaryId");

            migrationBuilder.CreateIndex(
                name: "IX_HourlyForecast_ForecastId_Hour",
                table: "HourlyForecast",
                columns: new[] { "ForecastId", "Hour" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HourlyForecast");

            migrationBuilder.DropTable(
                name: "DailyForecast");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "Summary");
        }
    }
}
