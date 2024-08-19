using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qinshit.DataAccess.Migrations
{
    public partial class ChangedColumnNameInContactsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Contacts",
                newName: "UserFullName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserFullName",
                table: "Contacts",
                newName: "FullName");
        }
    }
}
