using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Repositories.Migrations.WorkorderStatus
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkorderStatuses");
        }
    }
}
