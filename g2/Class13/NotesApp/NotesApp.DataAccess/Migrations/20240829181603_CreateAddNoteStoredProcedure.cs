using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesApp.DataAccess.Migrations
{
    public partial class CreateAddNoteStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_AddNote 
                    @Text nvarchar(250),
                    @Priority int,
                    @Tag int,
                    @UserId int
                AS
                BEGIN
                    INSERT INTO dbo.Note (Text, Priority, Tag, UserId)
                    VALUES (@Text, @Priority, @Tag, @UserId);
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE sp_AddNote");
        }
    }
}
