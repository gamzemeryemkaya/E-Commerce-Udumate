using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseBook.DataAccess.Migrations
{
    public partial class mig_add_updatecourse1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstructorDescription",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstructorImage",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstructorName",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstructorStatus",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstructorDescription",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "InstructorImage",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "InstructorName",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "InstructorStatus",
                table: "Courses");
        }
    }
}
