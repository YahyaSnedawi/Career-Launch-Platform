using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerLaunch.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployerAndJobSeeker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployerId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobSeekerId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "JobSeekerId",
                table: "AspNetUsers");
        }
    }
}
