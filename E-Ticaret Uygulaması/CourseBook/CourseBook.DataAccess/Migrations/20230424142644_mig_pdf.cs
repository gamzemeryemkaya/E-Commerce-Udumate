using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseBook.DataAccess.Migrations
{
    public partial class mig_pdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PdfUrl",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PdfUrl",
                table: "Courses");
        }
    }
}
