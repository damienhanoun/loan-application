using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Acquisition.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_projects_and_amounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Maturities");

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "Type", "Value" },
                values: new object[,]
                {
                    { 1, "maturity", "6" },
                    { 2, "maturity", "12" },
                    { 3, "maturity", "24" },
                    { 4, "maturity", "36" },
                    { 5, "maturity", "48" },
                    { 6, "maturity", "72" },
                    { 7, "maturity", "84" },
                    { 8, "amount", "1000" },
                    { 9, "amount", "1500" },
                    { 10, "amount", "2000" },
                    { 11, "amount", "2500" },
                    { 12, "amount", "3000" },
                    { 13, "amount", "3500" },
                    { 14, "amount", "4000" },
                    { 15, "amount", "4500" },
                    { 16, "amount", "5000" },
                    { 17, "amount", "5500" },
                    { 18, "amount", "6000" },
                    { 19, "amount", "6500" },
                    { 20, "amount", "7000" },
                    { 21, "amount", "7500" },
                    { 22, "amount", "8000" },
                    { 23, "amount", "8500" },
                    { 24, "amount", "9000" },
                    { 25, "amount", "9500" },
                    { 26, "amount", "+10000" },
                    { 27, "project", "Wedding" },
                    { 28, "project", "Home Renovation" },
                    { 29, "project", "Vacation" },
                    { 30, "project", "Debt Consolidation" },
                    { 31, "project", "Car Purchase" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.CreateTable(
                name: "Maturities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maturities", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Maturities",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 1, 6 },
                    { 2, 12 },
                    { 3, 24 },
                    { 4, 36 },
                    { 5, 48 },
                    { 6, 72 },
                    { 7, 84 }
                });
        }
    }
}
