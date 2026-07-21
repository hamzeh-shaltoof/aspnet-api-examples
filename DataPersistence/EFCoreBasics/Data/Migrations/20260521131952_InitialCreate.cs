using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCoreBasics.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Reviewer = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Stars = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Laptop", 750.00m },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Mouse", 25.50m },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Keyboard", 45.00m },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Monitor", 180.00m },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Headphones", 60.00m },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Webcam", 70.00m },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "Microphone", 95.00m },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "Printer", 150.00m },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "Tablet", 300.00m },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Smartphone", 650.00m },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Smartphone", 600.00m },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "Smartphone", 550.00m }
                });

            migrationBuilder.InsertData(
                table: "ProductReviews",
                columns: new[] { "Id", "ProductId", "Reviewer", "Stars" },
                values: new object[,]
                {
                    { new Guid("a0000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), "Ahmad", 5 },
                    { new Guid("a0000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111111"), "Sara", 4 },
                    { new Guid("a0000000-0000-0000-0000-000000000003"), new Guid("22222222-2222-2222-2222-222222222222"), "Ali", 3 },
                    { new Guid("a0000000-0000-0000-0000-000000000004"), new Guid("22222222-2222-2222-2222-222222222222"), "Lina", 5 },
                    { new Guid("a0000000-0000-0000-0000-000000000005"), new Guid("33333333-3333-3333-3333-333333333333"), "Omar", 4 },
                    { new Guid("a0000000-0000-0000-0000-000000000006"), new Guid("33333333-3333-3333-3333-333333333333"), "Noor", 5 },
                    { new Guid("a0000000-0000-0000-0000-000000000007"), new Guid("44444444-4444-4444-4444-444444444444"), "Huda", 4 },
                    { new Guid("a0000000-0000-0000-0000-000000000008"), new Guid("44444444-4444-4444-4444-444444444444"), "Yousef", 3 },
                    { new Guid("a0000000-0000-0000-0000-000000000009"), new Guid("55555555-5555-5555-5555-555555555555"), "Rana", 5 },
                    { new Guid("a0000000-0000-0000-0000-000000000010"), new Guid("55555555-5555-5555-5555-555555555555"), "Khaled", 4 },
                    { new Guid("a0000000-0000-0000-0000-000000000011"), new Guid("66666666-6666-6666-6666-666666666666"), "Zaid", 3 },
                    { new Guid("a0000000-0000-0000-0000-000000000012"), new Guid("66666666-6666-6666-6666-666666666666"), "Maya", 4 },
                    { new Guid("a0000000-0000-0000-0000-000000000013"), new Guid("77777777-7777-7777-7777-777777777777"), "Fadi", 5 },
                    { new Guid("a0000000-0000-0000-0000-000000000014"), new Guid("77777777-7777-7777-7777-777777777777"), "Dana", 4 },
                    { new Guid("a0000000-0000-0000-0000-000000000015"), new Guid("88888888-8888-8888-8888-888888888888"), "Sami", 3 },
                    { new Guid("a0000000-0000-0000-0000-000000000016"), new Guid("88888888-8888-8888-8888-888888888888"), "Reem", 5 },
                    { new Guid("a0000000-0000-0000-0000-000000000017"), new Guid("99999999-9999-9999-9999-999999999999"), "Tariq", 4 },
                    { new Guid("a0000000-0000-0000-0000-000000000018"), new Guid("99999999-9999-9999-9999-999999999999"), "Salma", 5 },
                    { new Guid("a0000000-0000-0000-0000-000000000019"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Nader", 4 },
                    { new Guid("a0000000-0000-0000-0000-000000000020"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Farah", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId",
                table: "ProductReviews",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductReviews");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
