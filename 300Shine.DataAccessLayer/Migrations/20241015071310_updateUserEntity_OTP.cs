﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _300Shine.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class updateUserEntity_OTP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Otp",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Otp",
                table: "User");
        }
    }
}
