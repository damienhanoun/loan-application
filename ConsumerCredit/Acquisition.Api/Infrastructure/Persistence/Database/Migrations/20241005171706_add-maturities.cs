using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Acquisition.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addmaturities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Maturities");
        }
    }
}
