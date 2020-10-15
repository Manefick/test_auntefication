using Microsoft.EntityFrameworkCore.Migrations;

namespace test_auntefication.Migrations.ProductsDb
{
    public partial class b : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BundleTabacoWeigh",
                table: "WorkStock",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BundleTabacoWeigh",
                table: "WorkStock");
        }
    }
}
