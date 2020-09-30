using Microsoft.EntityFrameworkCore.Migrations;

namespace test_auntefication.Migrations.ProductsDb
{
    public partial class DeleteConectionWithTabaco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyStock_Tabaco_TabacoId",
                table: "CompanyStock");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkStock_Tabaco_TabacoId",
                table: "WorkStock");

            migrationBuilder.DropIndex(
                name: "IX_WorkStock_TabacoId",
                table: "WorkStock");

            migrationBuilder.DropIndex(
                name: "IX_CompanyStock_TabacoId",
                table: "CompanyStock");

            migrationBuilder.DropColumn(
                name: "TabacoId",
                table: "WorkStock");

            migrationBuilder.DropColumn(
                name: "TabacoId",
                table: "CompanyStock");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TabacoId",
                table: "WorkStock",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TabacoId",
                table: "CompanyStock",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkStock_TabacoId",
                table: "WorkStock",
                column: "TabacoId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStock_TabacoId",
                table: "CompanyStock",
                column: "TabacoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyStock_Tabaco_TabacoId",
                table: "CompanyStock",
                column: "TabacoId",
                principalTable: "Tabaco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkStock_Tabaco_TabacoId",
                table: "WorkStock",
                column: "TabacoId",
                principalTable: "Tabaco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
