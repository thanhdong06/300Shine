using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _300Shine.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentSlot_Appointment_AppointmentId",
                table: "AppointmentSlot");

            migrationBuilder.RenameColumn(
                name: "AppointmentId",
                table: "AppointmentSlot",
                newName: "AppointmentDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentSlot_AppointmentId",
                table: "AppointmentSlot",
                newName: "IX_AppointmentSlot_AppointmentDetailId");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SalonId",
                table: "User",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalonId",
                table: "Service",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_User_SalonId",
                table: "User",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_SalonId",
                table: "Service",
                column: "SalonId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentSlot_AppointmentDetail_AppointmentDetailId",
                table: "AppointmentSlot",
                column: "AppointmentDetailId",
                principalTable: "AppointmentDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Salon_SalonId",
                table: "Service",
                column: "SalonId",
                principalTable: "Salon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Salon_SalonId",
                table: "User",
                column: "SalonId",
                principalTable: "Salon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentSlot_AppointmentDetail_AppointmentDetailId",
                table: "AppointmentSlot");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Salon_SalonId",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Salon_SalonId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_SalonId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Service_SalonId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SalonId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SalonId",
                table: "Service");

            migrationBuilder.RenameColumn(
                name: "AppointmentDetailId",
                table: "AppointmentSlot",
                newName: "AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentSlot_AppointmentDetailId",
                table: "AppointmentSlot",
                newName: "IX_AppointmentSlot_AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentSlot_Appointment_AppointmentId",
                table: "AppointmentSlot",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
