using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _300Shine.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class updateService_StapleWorking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "StapleWorking",
                newName: "Payday");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Service",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Service");

            migrationBuilder.RenameColumn(
                name: "Payday",
                table: "StapleWorking",
                newName: "Date");
        }
    }
}
