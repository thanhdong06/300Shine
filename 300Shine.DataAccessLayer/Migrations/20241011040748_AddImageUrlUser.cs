using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _300Shine.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "User");
        }
    }
}
