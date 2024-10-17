using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _300Shine.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppointment_detail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Appointment");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Service",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StylistId",
                table: "AppointmentDetail",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "AppointmentDetail",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "AppointmentDetail");

            migrationBuilder.AlterColumn<int>(
                name: "StylistId",
                table: "AppointmentDetail",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "Appointment",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Appointment",
                type: "text",
                nullable: true);
        }
    }
}
