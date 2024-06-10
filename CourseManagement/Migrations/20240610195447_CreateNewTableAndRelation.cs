using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.Migrations
{
    public partial class CreateNewTableAndRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SocialMedia1",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "SocialMedia2",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "SocialMedia3",
                table: "Instructors");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "SocialMedia1",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialMedia2",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialMedia3",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
