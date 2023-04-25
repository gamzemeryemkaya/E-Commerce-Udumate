using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseBook.DataAccess.Migrations
{
    public partial class mig_add_upnewcourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Detail",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InfoContent",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InfoTitle",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title1",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title2",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detail",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "InfoContent",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "InfoTitle",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Title1",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Title2",
                table: "Courses");
        }
    }
}
