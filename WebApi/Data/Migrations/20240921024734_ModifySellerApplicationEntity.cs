using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifySellerApplicationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "SellerApplication",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "BusinessRegistrationCertificateUrl",
                table: "SellerApplication",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "SellerApplication",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BusinessRegistrationCertificateUrl",
                table: "SellerApplication",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 20, 15, 22, 15, 290, DateTimeKind.Utc).AddTicks(9981), new DateTime(2024, 9, 20, 15, 22, 15, 290, DateTimeKind.Utc).AddTicks(9984) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 20, 15, 22, 15, 290, DateTimeKind.Utc).AddTicks(9988), new DateTime(2024, 9, 20, 15, 22, 15, 290, DateTimeKind.Utc).AddTicks(9989) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 20, 15, 22, 15, 290, DateTimeKind.Utc).AddTicks(9992), new DateTime(2024, 9, 20, 15, 22, 15, 290, DateTimeKind.Utc).AddTicks(9993) });
        }
    }
}
