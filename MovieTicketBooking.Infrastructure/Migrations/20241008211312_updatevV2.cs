using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieTicketBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatevV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cd1e2de-5c50-4c27-8d26-703bd491db99");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e79a250-4a6e-497c-97d7-d818b6b7fc82");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "95f652e5-0113-4424-a732-42f10fe179f6", "2", "User", "User" },
                    { "e04024ed-086e-4ec5-af59-220160c2ab2a", "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95f652e5-0113-4424-a732-42f10fe179f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e04024ed-086e-4ec5-af59-220160c2ab2a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1cd1e2de-5c50-4c27-8d26-703bd491db99", "1", "Admin", "Admin" },
                    { "1e79a250-4a6e-497c-97d7-d818b6b7fc82", "2", "User", "User" }
                });
        }
    }
}
