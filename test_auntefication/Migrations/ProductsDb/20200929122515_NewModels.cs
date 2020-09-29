using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace test_auntefication.Migrations.ProductsDb
{
    public partial class NewModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tabacos");

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tabaco",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabaco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyStock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TabacoId = table.Column<int>(nullable: false),
                    TabacoBundleWeigh = table.Column<int>(nullable: false),
                    TabacoCount = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyStock_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyStock_Tabaco_TabacoId",
                        column: x => x.TabacoId,
                        principalTable: "Tabaco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkStock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NameTabaco = table.Column<string>(nullable: true),
                    TabacoId = table.Column<int>(nullable: false),
                    TabacoWeigh = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkStock_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkStock_Tabaco_TabacoId",
                        column: x => x.TabacoId,
                        principalTable: "Tabaco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStock_CompanyId",
                table: "CompanyStock",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStock_TabacoId",
                table: "CompanyStock",
                column: "TabacoId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkStock_CompanyId",
                table: "WorkStock",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkStock_TabacoId",
                table: "WorkStock",
                column: "TabacoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyStock");

            migrationBuilder.DropTable(
                name: "WorkStock");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Tabaco");

            migrationBuilder.CreateTable(
                name: "Tabacos",
                columns: table => new
                {
                    TabacosID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabacos", x => x.TabacosID);
                });
        }
    }
}
