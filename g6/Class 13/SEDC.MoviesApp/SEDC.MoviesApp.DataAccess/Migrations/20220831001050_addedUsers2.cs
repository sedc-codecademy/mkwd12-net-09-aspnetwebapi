using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEDC.MoviesApp.DataAccess.Migrations
{
    public partial class addedUsers2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { 2, "Bob1", "Bobsky1", "(?\\?-??3#>L?q", "bob0071" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
