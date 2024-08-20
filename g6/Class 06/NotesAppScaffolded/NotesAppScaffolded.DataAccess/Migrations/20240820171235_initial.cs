using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesAppScaffolded.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //we need to comment this section because here we use the database first approach and these tables already exist

            //migrationBuilder.CreateTable(
            //    name: "User",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false),
            //        Firstname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        Lastname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_User", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Note",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false),
            //        Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        Tag = table.Column<int>(type: "int", nullable: true),
            //        Priority = table.Column<int>(type: "int", nullable: false),
            //        UserId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Note", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Note_User",
            //            column: x => x.UserId,
            //            principalTable: "User",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Note_UserId",
            //    table: "Note",
            //    column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
