﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerLaunch.Migrations
{
    /// <inheritdoc />
    public partial class AccountMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmployer",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsJobSeeker",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmployer",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsJobSeeker",
                table: "AspNetUsers");
        }
    }
}
