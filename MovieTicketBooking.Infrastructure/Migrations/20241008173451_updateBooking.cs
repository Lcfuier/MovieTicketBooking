using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieTicketBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa59f98f-5ea9-4d5d-b7ff-82cfe3eb9647");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e972551e-402f-4658-8cde-093559779c58");

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "Booking",
                type: "varchar(256)",
                unicode: false,
                maxLength: 256,
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "29e28df1-3c57-4461-8f30-2e6015ba9520", "1", "Admin", "Admin" },
                    { "82f11093-6ddb-4ff1-8b73-836db8f42470", "2", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29e28df1-3c57-4461-8f30-2e6015ba9520");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82f11093-6ddb-4ff1-8b73-836db8f42470");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Booking");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "aa59f98f-5ea9-4d5d-b7ff-82cfe3eb9647", "2", "User", "User" },
                    { "e972551e-402f-4658-8cde-093559779c58", "1", "Admin", "Admin" }
                });
        }
    }
}
