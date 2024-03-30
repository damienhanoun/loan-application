using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acquisition.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InitialLoanWish_Project = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    InitialLoanWish_Amount = table.Column<decimal>(type: "numeric", nullable: true),
                    InitialLoanWish_Maturity = table.Column<int>(type: "integer", nullable: true),
                    LoanOfferId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserInformation_Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanContracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanOffers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Maturity = table.Column<int>(type: "integer", nullable: false),
                    MonthlyAmount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanOffers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanApplications");

            migrationBuilder.DropTable(
                name: "LoanContracts");

            migrationBuilder.DropTable(
                name: "LoanOffers");
        }
    }
}
