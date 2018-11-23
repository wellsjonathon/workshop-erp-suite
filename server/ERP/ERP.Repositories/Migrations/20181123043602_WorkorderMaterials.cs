using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Repositories.Migrations
{
    public partial class WorkorderMaterials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialCategory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialCategory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MaterialType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    WebsiteURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Material",
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
                    table.PrimaryKey("PK_Material", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Material_MaterialCategory_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "MaterialCategory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Material_MaterialType_TypeID",
                        column: x => x.TypeID,
                        principalTable: "MaterialType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkorderMaterials",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WorkorderID = table.Column<int>(nullable: true),
                    MaterialID = table.Column<int>(nullable: true),
                    VendorID = table.Column<int>(nullable: true),
                    QuantityUsed = table.Column<float>(nullable: false),
                    CostPerUnit = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkorderMaterials", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkorderMaterials_Material_MaterialID",
                        column: x => x.MaterialID,
                        principalTable: "Material",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkorderMaterials_Vendor_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkorderMaterials_Workorders_WorkorderID",
                        column: x => x.WorkorderID,
                        principalTable: "Workorders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Material_CategoryID",
                table: "Material",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Material_TypeID",
                table: "Material",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkorderMaterials_MaterialID",
                table: "WorkorderMaterials",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkorderMaterials_VendorID",
                table: "WorkorderMaterials",
                column: "VendorID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkorderMaterials_WorkorderID",
                table: "WorkorderMaterials",
                column: "WorkorderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkorderMaterials");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropTable(
                name: "MaterialCategory");

            migrationBuilder.DropTable(
                name: "MaterialType");
        }
    }
}
