using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerLaunch.Migrations
{
    /// <inheritdoc />
    public partial class JobPostID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_JobPosts_JobId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_JobPosts_JobPostId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_JobId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Applications");

            migrationBuilder.AlterColumn<int>(
                name: "JobPostId",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                name: "FK_Applications_JobPosts_JobPostId",
                table: "Applications",
                column: "JobPostId",
                principalTable: "JobPosts",
                principalColumn: "JobPostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_JobPosts_JobPostId1",
                table: "Applications",
                column: "JobPostId1",
                principalTable: "JobPosts",
                principalColumn: "JobPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_JobPosts_JobPostId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_JobPosts_JobPostId1",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_JobPostId1",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "JobPostId1",
                table: "Applications");

            migrationBuilder.AlterColumn<int>(
                name: "JobPostId",
                table: "Applications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_JobId",
                table: "Applications",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_JobPosts_JobId",
                table: "Applications",
                column: "JobId",
                principalTable: "JobPosts",
                principalColumn: "JobPostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_JobPosts_JobPostId",
                table: "Applications",
                column: "JobPostId",
                principalTable: "JobPosts",
                principalColumn: "JobPostId");
        }
    }
}
