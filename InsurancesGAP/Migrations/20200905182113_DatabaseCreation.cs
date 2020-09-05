using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InsurancesGAP.Migrations
{
    public partial class DatabaseCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoverageType",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoverageType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RiskType",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Policy",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CoveragePercentage = table.Column<double>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    CoverageMonths = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    RiskTypeId = table.Column<long>(nullable: false),
                    CustomerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policy", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Policy_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Policy_RiskType_RiskTypeId",
                        column: x => x.RiskTypeId,
                        principalTable: "RiskType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PolicyCoverageType",
                columns: table => new
                {
                    CoverageTypeId = table.Column<long>(nullable: false),
                    PolicyId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyCoverageType", x => new { x.CoverageTypeId, x.PolicyId });
                    table.ForeignKey(
                        name: "FK_PolicyCoverageType_CoverageType_CoverageTypeId",
                        column: x => x.CoverageTypeId,
                        principalTable: "CoverageType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PolicyCoverageType_Policy_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policy",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Policy_CustomerId",
                table: "Policy",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_RiskTypeId",
                table: "Policy",
                column: "RiskTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyCoverageType_PolicyId",
                table: "PolicyCoverageType",
                column: "PolicyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PolicyCoverageType");

            migrationBuilder.DropTable(
                name: "CoverageType");

            migrationBuilder.DropTable(
                name: "Policy");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "RiskType");
        }
    }
}
