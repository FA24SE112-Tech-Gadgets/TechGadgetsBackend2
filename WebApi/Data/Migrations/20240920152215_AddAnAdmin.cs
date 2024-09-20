using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAnAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Address", "AvatarUrl", "CCCD", "CreatedAt", "DateOfBirth", "Email", "FullName", "Gender", "LoginMethod", "Password", "PhoneNumber", "Role", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, null, null, null, new DateTime(2024, 9, 20, 15, 22, 15, 290, DateTimeKind.Utc).AddTicks(9981), null, "admin1@gmail.com", "Admin 1", null, "Default", "5wJ0xMM/o1DPaTby8haqjIeEx0hqnJfyw4SmivYCGT17khWSPTXkR+56laWZr3/U", null, "Admin", "Active", new DateTime(2024, 9, 20, 15, 22, 15, 290, DateTimeKind.Utc).AddTicks(9984) },
                    { 2, null, null, null, new DateTime(2024, 9, 20, 15, 22, 15, 290, DateTimeKind.Utc).AddTicks(9988), null, "admin2@gmail.com", "Admin 2", null, "Default", "5wJ0xMM/o1DPaTby8haqjIeEx0hqnJfyw4SmivYCGT17khWSPTXkR+56laWZr3/U", null, "Admin", "Active", new DateTime(2024, 9, 20, 15, 22, 15, 290, DateTimeKind.Utc).AddTicks(9989) },
                    { 3, null, null, null, new DateTime(2024, 9, 20, 15, 22, 15, 290, DateTimeKind.Utc).AddTicks(9992), null, "admin3@gmail.com", "Admin 3", null, "Default", "5wJ0xMM/o1DPaTby8haqjIeEx0hqnJfyw4SmivYCGT17khWSPTXkR+56laWZr3/U", null, "Admin", "Active", new DateTime(2024, 9, 20, 15, 22, 15, 290, DateTimeKind.Utc).AddTicks(9993) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
