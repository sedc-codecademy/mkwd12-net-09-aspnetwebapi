using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qinshift.EShop.DataAccess.Migrations
{
    public partial class UsersAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 36, 12, 805, DateTimeKind.Local).AddTicks(9771), new DateTime(2024, 9, 2, 18, 36, 12, 805, DateTimeKind.Local).AddTicks(9814) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 36, 12, 805, DateTimeKind.Local).AddTicks(9820), new DateTime(2024, 9, 2, 18, 36, 12, 805, DateTimeKind.Local).AddTicks(9822) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 36, 12, 805, DateTimeKind.Local).AddTicks(9824), new DateTime(2024, 9, 2, 18, 36, 12, 805, DateTimeKind.Local).AddTicks(9826) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 36, 12, 805, DateTimeKind.Local).AddTicks(9849), new DateTime(2024, 9, 2, 18, 36, 12, 805, DateTimeKind.Local).AddTicks(9851) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 36, 12, 805, DateTimeKind.Local).AddTicks(9857), new DateTime(2024, 9, 2, 18, 36, 12, 805, DateTimeKind.Local).AddTicks(9859) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 36, 12, 805, DateTimeKind.Local).AddTicks(9861), new DateTime(2024, 9, 2, 18, 36, 12, 805, DateTimeKind.Local).AddTicks(9863) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 36, 12, 805, DateTimeKind.Local).AddTicks(9881), new DateTime(2024, 9, 2, 18, 36, 12, 805, DateTimeKind.Local).AddTicks(9883) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 9, 2, 18, 36, 12, 805, DateTimeKind.Local).AddTicks(9888), new DateTime(2024, 9, 2, 18, 36, 12, 805, DateTimeKind.Local).AddTicks(9890) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "FirstName", "LastName", "ModifiedBy", "ModifiedOn", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 2, 18, 36, 12, 805, DateTimeKind.Local).AddTicks(9929), "John", "Doe", 1, new DateTime(2024, 9, 2, 18, 36, 12, 805, DateTimeKind.Local).AddTicks(9932), "test123#", "j_doe" },
                    { 2, 1, new DateTime(2024, 9, 2, 18, 36, 12, 805, DateTimeKind.Local).AddTicks(9940), "Bob", "Bobsky", 1, new DateTime(2024, 9, 2, 18, 36, 12, 805, DateTimeKind.Local).AddTicks(9942), "test1234#", "bob_bobsky" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 8, 25, 11, 49, 50, 600, DateTimeKind.Local).AddTicks(2800), new DateTime(2024, 8, 25, 11, 49, 50, 600, DateTimeKind.Local).AddTicks(2834) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 8, 25, 11, 49, 50, 600, DateTimeKind.Local).AddTicks(2840), new DateTime(2024, 8, 25, 11, 49, 50, 600, DateTimeKind.Local).AddTicks(2842) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 8, 25, 11, 49, 50, 600, DateTimeKind.Local).AddTicks(2844), new DateTime(2024, 8, 25, 11, 49, 50, 600, DateTimeKind.Local).AddTicks(2846) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 8, 25, 11, 49, 50, 600, DateTimeKind.Local).AddTicks(2868), new DateTime(2024, 8, 25, 11, 49, 50, 600, DateTimeKind.Local).AddTicks(2870) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 8, 25, 11, 49, 50, 600, DateTimeKind.Local).AddTicks(2877), new DateTime(2024, 8, 25, 11, 49, 50, 600, DateTimeKind.Local).AddTicks(2879) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 8, 25, 11, 49, 50, 600, DateTimeKind.Local).AddTicks(2882), new DateTime(2024, 8, 25, 11, 49, 50, 600, DateTimeKind.Local).AddTicks(2883) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 8, 25, 11, 49, 50, 600, DateTimeKind.Local).AddTicks(2904), new DateTime(2024, 8, 25, 11, 49, 50, 600, DateTimeKind.Local).AddTicks(2906) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 8, 25, 11, 49, 50, 600, DateTimeKind.Local).AddTicks(2911), new DateTime(2024, 8, 25, 11, 49, 50, 600, DateTimeKind.Local).AddTicks(2912) });
        }
    }
}
