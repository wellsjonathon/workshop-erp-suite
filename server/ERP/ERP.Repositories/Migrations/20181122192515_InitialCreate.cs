using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Repositories.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkorderStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkorderStatuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Workorders",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StatusID = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    QuoteRequested = table.Column<bool>(nullable: false),
                    TotalMaterialCost = table.Column<decimal>(nullable: false),
                    TotalLabourCost = table.Column<decimal>(nullable: false),
                    TotalCost = table.Column<decimal>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateRequiredBy = table.Column<DateTime>(nullable: false),
                    DateCompleted = table.Column<DateTime>(nullable: false),
                    DatePickedUp = table.Column<DateTime>(nullable: false),
                    ClientPhoneNumber = table.Column<string>(nullable: true),
                    ClientEmail = table.Column<string>(nullable: true),
                    ClientName = table.Column<string>(nullable: true),
                    EstMaterialCost = table.Column<decimal>(nullable: false),
                    EstLabourCost = table.Column<decimal>(nullable: false),
                    EstOtherCost = table.Column<decimal>(nullable: false),
                    EstTotalCost = table.Column<decimal>(nullable: false),
                    EstDeliveryDate = table.Column<DateTime>(nullable: false),
                    AuthorizedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workorders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Workorders_WorkorderStatuses_StatusID",
                        column: x => x.StatusID,
                        principalTable: "WorkorderStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "WorkorderStatuses",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "New" },
                    { 2, "Scheduled" },
                    { 3, "In Progress" },
                    { 4, "Completed" },
                    { 5, "Rejected" },
                    { 6, "Cancelled" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workorders_StatusID",
                table: "Workorders",
                column: "StatusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Workorders");

            migrationBuilder.DropTable(
                name: "WorkorderStatuses");
        }
    }
}
