using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieTicketBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateSeat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinema_BookingDetail_BookingDetailId",
                table: "Cinema");

            migrationBuilder.DropForeignKey(
                name: "FK_Movie_BookingDetail_BookingDetailId",
                table: "Movie");

            migrationBuilder.DropForeignKey(
                name: "FK_Seat_BookingDetail_BookingDetailId",
                table: "Seat");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowTime_BookingDetail_BookingDetailId",
                table: "ShowTime");

            migrationBuilder.DropIndex(
                name: "IX_ShowTime_BookingDetailId",
                table: "ShowTime");

            migrationBuilder.DropIndex(
                name: "IX_Seat_BookingDetailId",
                table: "Seat");

            migrationBuilder.DropIndex(
                name: "IX_Movie_BookingDetailId",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_Cinema_BookingDetailId",
                table: "Cinema");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9cb5bdd-9b8d-4ba8-8335-ae261fc0926e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eedf9bd4-f007-4f13-b6e8-757264bb6a65");

            migrationBuilder.DropColumn(
                name: "BookingDetailId",
                table: "ShowTime");

            migrationBuilder.DropColumn(
                name: "BookingDetailId",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "BookingDetailId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "BookingDetailId",
                table: "Cinema");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Seat",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "CinemaId",
                table: "BookingDetail",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MovieId",
                table: "BookingDetail",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SeatId",
                table: "BookingDetail",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShowTimeId",
                table: "BookingDetail",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "aa59f98f-5ea9-4d5d-b7ff-82cfe3eb9647", "2", "User", "User" },
                    { "e972551e-402f-4658-8cde-093559779c58", "1", "Admin", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetail_CinemaId",
                table: "BookingDetail",
                column: "CinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetail_MovieId",
                table: "BookingDetail",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetail_SeatId",
                table: "BookingDetail",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetail_ShowTimeId",
                table: "BookingDetail",
                column: "ShowTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetail_Cinema_CinemaId",
                table: "BookingDetail",
                column: "CinemaId",
                principalTable: "Cinema",
                principalColumn: "CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetail_Movie_MovieId",
                table: "BookingDetail",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetail_Seat_SeatId",
                table: "BookingDetail",
                column: "SeatId",
                principalTable: "Seat",
                principalColumn: "SeatId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetail_ShowTime_ShowTimeId",
                table: "BookingDetail",
                column: "ShowTimeId",
                principalTable: "ShowTime",
                principalColumn: "ShowTimeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetail_Cinema_CinemaId",
                table: "BookingDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetail_Movie_MovieId",
                table: "BookingDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetail_Seat_SeatId",
                table: "BookingDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetail_ShowTime_ShowTimeId",
                table: "BookingDetail");

            migrationBuilder.DropIndex(
                name: "IX_BookingDetail_CinemaId",
                table: "BookingDetail");

            migrationBuilder.DropIndex(
                name: "IX_BookingDetail_MovieId",
                table: "BookingDetail");

            migrationBuilder.DropIndex(
                name: "IX_BookingDetail_SeatId",
                table: "BookingDetail");

            migrationBuilder.DropIndex(
                name: "IX_BookingDetail_ShowTimeId",
                table: "BookingDetail");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa59f98f-5ea9-4d5d-b7ff-82cfe3eb9647");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e972551e-402f-4658-8cde-093559779c58");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Seat");

            migrationBuilder.DropColumn(
                name: "CinemaId",
                table: "BookingDetail");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "BookingDetail");

            migrationBuilder.DropColumn(
                name: "SeatId",
                table: "BookingDetail");

            migrationBuilder.DropColumn(
                name: "ShowTimeId",
                table: "BookingDetail");

            migrationBuilder.AddColumn<Guid>(
                name: "BookingDetailId",
                table: "ShowTime",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BookingDetailId",
                table: "Seat",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BookingDetailId",
                table: "Movie",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BookingDetailId",
                table: "Cinema",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d9cb5bdd-9b8d-4ba8-8335-ae261fc0926e", "2", "User", "User" },
                    { "eedf9bd4-f007-4f13-b6e8-757264bb6a65", "1", "Admin", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShowTime_BookingDetailId",
                table: "ShowTime",
                column: "BookingDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_BookingDetailId",
                table: "Seat",
                column: "BookingDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_BookingDetailId",
                table: "Movie",
                column: "BookingDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Cinema_BookingDetailId",
                table: "Cinema",
                column: "BookingDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinema_BookingDetail_BookingDetailId",
                table: "Cinema",
                column: "BookingDetailId",
                principalTable: "BookingDetail",
                principalColumn: "BookingDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_BookingDetail_BookingDetailId",
                table: "Movie",
                column: "BookingDetailId",
                principalTable: "BookingDetail",
                principalColumn: "BookingDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seat_BookingDetail_BookingDetailId",
                table: "Seat",
                column: "BookingDetailId",
                principalTable: "BookingDetail",
                principalColumn: "BookingDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTime_BookingDetail_BookingDetailId",
                table: "ShowTime",
                column: "BookingDetailId",
                principalTable: "BookingDetail",
                principalColumn: "BookingDetailId");
        }
    }
}
