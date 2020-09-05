using Microsoft.EntityFrameworkCore.Migrations;

namespace InsurancesGAP.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CoverageType",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1L, "Terremoto" },
                    { 2L, "Incendio" },
                    { 3L, "Robo" },
                    { 4L, "Pérdida" }
                });

            migrationBuilder.InsertData(
                table: "RiskType",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1L, "Bajo" },
                    { 2L, "Medio" },
                    { 3L, "Medio-Alto" },
                    { 4L, "Alto" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CoverageType",
                keyColumn: "ID",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "CoverageType",
                keyColumn: "ID",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "CoverageType",
                keyColumn: "ID",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "CoverageType",
                keyColumn: "ID",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "RiskType",
                keyColumn: "ID",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "RiskType",
                keyColumn: "ID",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "RiskType",
                keyColumn: "ID",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "RiskType",
                keyColumn: "ID",
                keyValue: 4L);
        }
    }
}
