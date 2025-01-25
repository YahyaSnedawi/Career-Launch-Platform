using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerLaunch.Migrations
{
    /// <inheritdoc />
    public partial class AddForeginKeyApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_JobPosts_JobPostId1",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_JobPostId1",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "JobPostId1",
                table: "Applications");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "JobPosts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobPosts_ApplicationId",
                table: "JobPosts",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosts_Applications_ApplicationId",
                table: "JobPosts",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "ApplicationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPosts_Applications_ApplicationId",
                table: "JobPosts");

            migrationBuilder.DropIndex(
                name: "IX_JobPosts_ApplicationId",
                table: "JobPosts");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "JobPosts");

            migrationBuilder.AddColumn<int>(
                name: "JobPostId1",
                table: "Applications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_JobPostId1",
                table: "Applications",
                column: "JobPostId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_JobPosts_JobPostId1",
                table: "Applications",
                column: "JobPostId1",
                principalTable: "JobPosts",
                principalColumn: "JobPostId");
        }
    }
}
