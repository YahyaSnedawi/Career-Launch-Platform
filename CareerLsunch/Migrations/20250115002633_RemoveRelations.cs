using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerLaunch.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_EmployerId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_JobSeekerId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_EmployerId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_JobSeekerId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "JobSeekerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "JobSeekerId",
                table: "Applications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "EmployerId",
                table: "Applications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobSeekerId",
                table: "Applications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_EmployerId",
                table: "Applications",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_JobSeekerId",
                table: "Applications",
                column: "JobSeekerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_EmployerId",
                table: "Applications",
                column: "EmployerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_JobSeekerId",
                table: "Applications",
                column: "JobSeekerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
