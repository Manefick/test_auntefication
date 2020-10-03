using Microsoft.EntityFrameworkCore.Migrations;

namespace test_auntefication.Migrations.ProductsDb
{
    public partial class UserCompanyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserCompany",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserCompany",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
