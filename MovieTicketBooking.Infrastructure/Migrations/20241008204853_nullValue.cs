using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieTicketBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class nullValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29e28df1-3c57-4461-8f30-2e6015ba9520");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82f11093-6ddb-4ff1-8b73-836db8f42470");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedTime",
                table: "Movie",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b92c7b89-f169-4868-a67d-16c37bf23720", "2", "User", "User" },
                    { "bc91c75b-aabc-42e1-b6c8-cb33aabb8531", "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b92c7b89-f169-4868-a67d-16c37bf23720");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc91c75b-aabc-42e1-b6c8-cb33aabb8531");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedTime",
                table: "Movie",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "29e28df1-3c57-4461-8f30-2e6015ba9520", "1", "Admin", "Admin" },
                    { "82f11093-6ddb-4ff1-8b73-836db8f42470", "2", "User", "User" }
                });
        }
    }
}
