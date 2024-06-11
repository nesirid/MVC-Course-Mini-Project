using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.Migrations
{
    public partial class CorrectionSocialMediaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstructorSocialMedias_SocialMedias_SocialMediaId1",
                table: "InstructorSocialMedias");

            migrationBuilder.DropIndex(
                name: "IX_InstructorSocialMedias_SocialMediaId1",
                table: "InstructorSocialMedias");

            migrationBuilder.DropColumn(
                name: "SocialMediaId1",
                table: "InstructorSocialMedias");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "SocialMedias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "SocialMedias");

            migrationBuilder.AddColumn<int>(
                name: "SocialMediaId1",
                table: "InstructorSocialMedias",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstructorSocialMedias_SocialMediaId1",
                table: "InstructorSocialMedias",
                column: "SocialMediaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorSocialMedias_SocialMedias_SocialMediaId1",
                table: "InstructorSocialMedias",
                column: "SocialMediaId1",
                principalTable: "SocialMedias",
                principalColumn: "Id");
        }
    }
}
