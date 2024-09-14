using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesApp.DataAccess.Migrations
{
    public partial class remove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Note",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Note",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { 1, "Bob", "Bobsky", "password123", "bobbobsky" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { 2, "Jane", "Smith", "password123", "janesmith" });

            migrationBuilder.InsertData(
                table: "Note",
                columns: new[] { "Id", "Priority", "Tag", "Text", "UserId" },
                values: new object[] { 1, 3, 1, "Complete project report", 1 });

            migrationBuilder.InsertData(
                table: "Note",
                columns: new[] { "Id", "Priority", "Tag", "Text", "UserId" },
                values: new object[] { 2, 2, 2, "Go to the gym", 2 });
        }
    }
}
