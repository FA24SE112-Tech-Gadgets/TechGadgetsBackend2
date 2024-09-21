using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBusinessModelEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seller_BusinessModel_BusinessModelId",
                table: "Seller");

            migrationBuilder.DropForeignKey(
                name: "FK_SellerApplication_BusinessModel_BusinessModelId",
                table: "SellerApplication");

            migrationBuilder.DropTable(
                name: "BusinessModel");

            migrationBuilder.DropIndex(
                name: "IX_SellerApplication_BusinessModelId",
                table: "SellerApplication");

            migrationBuilder.DropIndex(
                name: "IX_Seller_BusinessModelId",
                table: "Seller");

            migrationBuilder.DropColumn(
                name: "BusinessModelId",
                table: "SellerApplication");

            migrationBuilder.DropColumn(
                name: "BusinessModelId",
                table: "Seller");

            migrationBuilder.AddColumn<string>(
                name: "BusinessModel",
                table: "SellerApplication",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BusinessModel",
                table: "Seller",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 14, 5, 37, 42, 345, DateTimeKind.Utc), new DateTime(2023, 9, 14, 5, 37, 42, 345, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 14, 5, 37, 42, 345, DateTimeKind.Utc), new DateTime(2023, 9, 14, 5, 37, 42, 345, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 9, 14, 5, 37, 42, 345, DateTimeKind.Utc), new DateTime(2023, 9, 14, 5, 37, 42, 345, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessModel",
                table: "SellerApplication");

            migrationBuilder.DropColumn(
                name: "BusinessModel",
                table: "Seller");

            migrationBuilder.AddColumn<int>(
                name: "BusinessModelId",
                table: "SellerApplication",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BusinessModelId",
                table: "Seller",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BusinessModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessModel", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 21, 2, 47, 34, 326, DateTimeKind.Utc).AddTicks(2452), new DateTime(2024, 9, 21, 2, 47, 34, 326, DateTimeKind.Utc).AddTicks(2456) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 21, 2, 47, 34, 326, DateTimeKind.Utc).AddTicks(2459), new DateTime(2024, 9, 21, 2, 47, 34, 326, DateTimeKind.Utc).AddTicks(2460) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 21, 2, 47, 34, 326, DateTimeKind.Utc).AddTicks(2463), new DateTime(2024, 9, 21, 2, 47, 34, 326, DateTimeKind.Utc).AddTicks(2463) });

            migrationBuilder.CreateIndex(
                name: "IX_SellerApplication_BusinessModelId",
                table: "SellerApplication",
                column: "BusinessModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Seller_BusinessModelId",
                table: "Seller",
                column: "BusinessModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seller_BusinessModel_BusinessModelId",
                table: "Seller",
                column: "BusinessModelId",
                principalTable: "BusinessModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SellerApplication_BusinessModel_BusinessModelId",
                table: "SellerApplication",
                column: "BusinessModelId",
                principalTable: "BusinessModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
