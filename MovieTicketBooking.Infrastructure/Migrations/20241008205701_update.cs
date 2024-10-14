using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieTicketBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_ShowTime_ShowTimeId",
                table: "Seat");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b92c7b89-f169-4868-a67d-16c37bf23720");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc91c75b-aabc-42e1-b6c8-cb33aabb8531");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShowTimeId",
                table: "Seat",
                type: "uniqueidentifier",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 128);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1cd1e2de-5c50-4c27-8d26-703bd491db99", "1", "Admin", "Admin" },
                    { "1e79a250-4a6e-497c-97d7-d818b6b7fc82", "2", "User", "User" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_ShowTime_ShowTimeId",
                table: "Seat",
                column: "ShowTimeId",
                principalTable: "ShowTime",
                principalColumn: "ShowTimeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seat_ShowTime_ShowTimeId",
                table: "Seat");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cd1e2de-5c50-4c27-8d26-703bd491db99");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e79a250-4a6e-497c-97d7-d818b6b7fc82");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShowTimeId",
                table: "Seat",
                type: "uniqueidentifier",
                maxLength: 128,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b92c7b89-f169-4868-a67d-16c37bf23720", "2", "User", "User" },
                    { "bc91c75b-aabc-42e1-b6c8-cb33aabb8531", "1", "Admin", "Admin" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_ShowTime_ShowTimeId",
                table: "Seat",
                column: "ShowTimeId",
                principalTable: "ShowTime",
                principalColumn: "ShowTimeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
