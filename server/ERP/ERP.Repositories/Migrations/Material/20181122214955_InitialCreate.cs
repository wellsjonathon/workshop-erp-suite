using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Repositories.Migrations.Material
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialCategories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MaterialTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CategoryID = table.Column<int>(nullable: true),
                    TypeID = table.Column<int>(nullable: true),
                    QuantityInStock = table.Column<float>(nullable: false),
                    Details = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Materials_MaterialCategories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "MaterialCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_MaterialTypes_TypeID",
                        column: x => x.TypeID,
                        principalTable: "MaterialTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "MaterialCategories",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Rod" },
                    { 2, "Plate" },
                    { 3, "Sheet" }
                });

            migrationBuilder.InsertData(
                table: "MaterialTypes",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Aluminum" },
                    { 2, "Steel" },
                    { 3, "Nylon" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_CategoryID",
                table: "Materials",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_TypeID",
                table: "Materials",
                column: "TypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "MaterialCategories");

            migrationBuilder.DropTable(
                name: "MaterialTypes");
        }
    }
}
