using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qinshift.MovieRent.DataAccess.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plot = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CreatedOn", "Genre", "Plot", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 26, 19, 41, 2, 956, DateTimeKind.Local).AddTicks(2638), 1, "A concierge teams up with one of his employees to prove his innocence after he is framed for murder.", new DateTime(2014, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Grand Budapest Hotel" },
                    { 2, new DateTime(2024, 8, 26, 19, 41, 2, 956, DateTimeKind.Local).AddTicks(2686), 1, "Two high school friends attempt to make it to a party before they go off to college.", new DateTime(2007, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Superbad" },
                    { 3, new DateTime(2024, 8, 26, 19, 41, 2, 956, DateTimeKind.Local).AddTicks(2689), 2, "In a post-apocalyptic world, Max teams up with Furiosa to escape a tyrant and his army.", new DateTime(2015, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mad Max: Fury Road" },
                    { 4, new DateTime(2024, 8, 26, 19, 41, 2, 956, DateTimeKind.Local).AddTicks(2692), 2, "A NYPD officer tries to save his wife and others taken hostage by German terrorists during a Christmas party.", new DateTime(1988, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Die Hard" },
                    { 5, new DateTime(2024, 8, 26, 19, 41, 2, 956, DateTimeKind.Local).AddTicks(2694), 3, "A man becomes the focus of an intense media circus when his wife disappears and he is suspected of murder.", new DateTime(2014, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gone Girl" },
                    { 6, new DateTime(2024, 8, 26, 19, 41, 2, 956, DateTimeKind.Local).AddTicks(2699), 3, "A thief who enters the dreams of others is given the chance to erase his criminal record by planting an idea into someone's subconscious.", new DateTime(2010, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Inception" },
                    { 7, new DateTime(2024, 8, 26, 19, 41, 2, 956, DateTimeKind.Local).AddTicks(2701), 4, "Paranormal investigators help a family terrorized by a dark presence in their farmhouse.", new DateTime(2013, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Conjuring" },
                    { 8, new DateTime(2024, 8, 26, 19, 41, 2, 956, DateTimeKind.Local).AddTicks(2704), 4, "A young African-American man visits his white girlfriend's family estate, where he uncovers a disturbing secret.", new DateTime(2017, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Get Out" },
                    { 9, new DateTime(2024, 8, 26, 19, 41, 2, 956, DateTimeKind.Local).AddTicks(2706), 5, "Two imprisoned men bond over a number of ReleaseDates, finding solace and eventual redemption through acts of common decency.", new DateTime(1994, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Shawshank Redemption" },
                    { 10, new DateTime(2024, 8, 26, 19, 41, 2, 956, DateTimeKind.Local).AddTicks(2709), 5, "The story of a man with a low IQ, who achieves great things in life despite the odds.", new DateTime(1994, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Forrest Gump" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
