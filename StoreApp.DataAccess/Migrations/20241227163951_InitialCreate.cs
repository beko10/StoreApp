using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StoreApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { "943b4f45-4a0f-48e2-a901-c7cc947c0a29", new DateTime(2024, 12, 27, 19, 39, 51, 123, DateTimeKind.Local).AddTicks(5094), "Mobile Phones", new DateTime(2024, 12, 27, 19, 39, 51, 123, DateTimeKind.Local).AddTicks(5105), "Phones" },
                    { "f8d3d601-03ab-459b-9504-94851fa5d2a6", new DateTime(2024, 12, 27, 19, 39, 51, 123, DateTimeKind.Local).AddTicks(5109), "Notebook Computers", new DateTime(2024, 12, 27, 19, 39, 51, 123, DateTimeKind.Local).AddTicks(5109), "Laptops" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "ImageUrl", "ModifiedDate", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { "51ab4e9e-bd33-476e-ab06-2de86f0bb937", "943b4f45-4a0f-48e2-a901-c7cc947c0a29", new DateTime(2024, 12, 27, 19, 39, 51, 123, DateTimeKind.Local).AddTicks(6985), "Apple iPhone 14 128GB", "images/iphone14.jpg", new DateTime(2024, 12, 27, 19, 39, 51, 123, DateTimeKind.Local).AddTicks(6989), "iPhone 14", 999.99m, 100 },
                    { "e809d187-f851-4228-bb80-35e057cea01b", "f8d3d601-03ab-459b-9504-94851fa5d2a6", new DateTime(2024, 12, 27, 19, 39, 51, 123, DateTimeKind.Local).AddTicks(6993), "Samsung Galaxy S23 256GB", "images/galaxys23.jpg", new DateTime(2024, 12, 27, 19, 39, 51, 123, DateTimeKind.Local).AddTicks(6994), "Samsung Galaxy S23", 899.99m, 75 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
