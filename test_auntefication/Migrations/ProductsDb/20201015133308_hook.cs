using Microsoft.EntityFrameworkCore.Migrations;

namespace test_auntefication.Migrations.ProductsDb
{
    public partial class hook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HookahMaster",
                table: "WorkStock",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NominalWeigth",
                table: "Tabaco",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HookahMaster",
                table: "WorkStock");

            migrationBuilder.DropColumn(
                name: "NominalWeigth",
                table: "Tabaco");
        }
    }
}
