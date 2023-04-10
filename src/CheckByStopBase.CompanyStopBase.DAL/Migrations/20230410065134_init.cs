using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CheckByStopBase.CompanyStopBase.DAL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "company_registry",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    add_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    tax_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    company_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company_registry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "company_report",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    partner = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company_report", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "company_report_position",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaxNumber = table.Column<string>(type: "text", nullable: false),
                    company_type = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    ReportId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company_report_position", x => x.Id);
                    table.ForeignKey(
                        name: "FK_company_report_position_company_report_ReportId",
                        column: x => x.ReportId,
                        principalTable: "company_report",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_company_registry_tax_number",
                table: "company_registry",
                column: "tax_number");

            migrationBuilder.CreateIndex(
                name: "IX_company_report_position_ReportId",
                table: "company_report_position",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_company_report_position_TaxNumber",
                table: "company_report_position",
                column: "TaxNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "company_registry");

            migrationBuilder.DropTable(
                name: "company_report_position");

            migrationBuilder.DropTable(
                name: "company_report");
        }
    }
}
