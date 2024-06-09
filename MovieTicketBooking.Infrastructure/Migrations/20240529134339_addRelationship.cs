using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieTicketBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BookingId",
                table: "Cinema",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cinema_BookingId",
                table: "Cinema",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinema_Booking_BookingId",
                table: "Cinema",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "BookingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinema_Booking_BookingId",
                table: "Cinema");

            migrationBuilder.DropIndex(
                name: "IX_Cinema_BookingId",
                table: "Cinema");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Cinema");
        }
    }
}
