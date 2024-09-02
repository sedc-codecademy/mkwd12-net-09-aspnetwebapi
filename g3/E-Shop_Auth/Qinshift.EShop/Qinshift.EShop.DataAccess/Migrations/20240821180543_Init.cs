using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qinshift.EShop.DataAccess.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[] { 1, 1, new DateTime(2024, 8, 21, 20, 5, 43, 187, DateTimeKind.Local).AddTicks(5705), "All sort of smart phones and tablets", 1, new DateTime(2024, 8, 21, 20, 5, 43, 187, DateTimeKind.Local).AddTicks(5741), "Smartphones and Tablets" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[] { 2, 1, new DateTime(2024, 8, 21, 20, 5, 43, 187, DateTimeKind.Local).AddTicks(5747), "Different brands of PCs and all type of hardware components.", 1, new DateTime(2024, 8, 21, 20, 5, 43, 187, DateTimeKind.Local).AddTicks(5749), "PC and hardware" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[] { 3, 1, new DateTime(2024, 8, 21, 20, 5, 43, 187, DateTimeKind.Local).AddTicks(5751), "Different brands of laptops", 1, new DateTime(2024, 8, 21, 20, 5, 43, 187, DateTimeKind.Local).AddTicks(5753), "Laptops" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedBy", "CreatedOn", "Description", "ImageUrl", "ModifiedBy", "ModifiedOn", "Name", "Price", "StockQuantity" },
                values: new object[] { 1, 1, 1, new DateTime(2024, 8, 21, 20, 5, 43, 187, DateTimeKind.Local).AddTicks(5777), "256GB | 5.8' | 8GB RAM", "iphone15.jpg", 1, new DateTime(2024, 8, 21, 20, 5, 43, 187, DateTimeKind.Local).AddTicks(5779), "Iphone 15 Pro 256GB", 1000m, 50 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedBy", "CreatedOn", "Description", "ImageUrl", "ModifiedBy", "ModifiedOn", "Name", "Price", "StockQuantity" },
                values: new object[] { 2, 3, 1, new DateTime(2024, 8, 21, 20, 5, 43, 187, DateTimeKind.Local).AddTicks(5785), "500GB SSD | 17' | 16GB RAM", "lenovoY700.jpg", 1, new DateTime(2024, 8, 21, 20, 5, 43, 187, DateTimeKind.Local).AddTicks(5787), "Lenovo Y700 Gaming laptop", 2000m, 20 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedBy", "CreatedOn", "Description", "ImageUrl", "ModifiedBy", "ModifiedOn", "Name", "Price", "StockQuantity" },
                values: new object[] { 3, 2, 1, new DateTime(2024, 8, 21, 20, 5, 43, 187, DateTimeKind.Local).AddTicks(5789), "16GB VRAM", "graficka.jpg", 1, new DateTime(2024, 8, 21, 20, 5, 43, 187, DateTimeKind.Local).AddTicks(5791), "NVIDIA GeForce 4090", 2200m, 10 });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "CreatedBy", "CreatedOn", "ImageUrl", "ModifiedBy", "ModifiedOn", "ProductId", "Rating", "ReviewerName" },
                values: new object[] { 1, "Very nice product", 1, new DateTime(2024, 8, 21, 20, 5, 43, 187, DateTimeKind.Local).AddTicks(5813), "images/phone.jpg", 1, new DateTime(2024, 8, 21, 20, 5, 43, 187, DateTimeKind.Local).AddTicks(5815), 1, 5, "Martin" });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "CreatedBy", "CreatedOn", "ImageUrl", "ModifiedBy", "ModifiedOn", "ProductId", "Rating", "ReviewerName" },
                values: new object[] { 2, "Bad product. It is very slow. The ram is too much used", 1, new DateTime(2024, 8, 21, 20, 5, 43, 187, DateTimeKind.Local).AddTicks(5820), "images/phone.jpg", 1, new DateTime(2024, 8, 21, 20, 5, 43, 187, DateTimeKind.Local).AddTicks(5822), 1, 2, "Slave" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
