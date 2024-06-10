using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.Migrations
{
    public partial class AddInstructorTableAddCourseRelAddCorseTableNewOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePictureUrl",
                table: "Instructors",
                newName: "SocialMedia3");

            migrationBuilder.RenameColumn(
                name: "Bio",
                table: "Instructors",
                newName: "SocialMedia2");

            migrationBuilder.AddColumn<string>(
                name: "Expertise",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialMedia1",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expertise",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "SocialMedia1",
                table: "Instructors");

            migrationBuilder.RenameColumn(
                name: "SocialMedia3",
                table: "Instructors",
                newName: "ProfilePictureUrl");

            migrationBuilder.RenameColumn(
                name: "SocialMedia2",
                table: "Instructors",
                newName: "Bio");
        }
    }
}
