using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseBook.DataAccess.Migrations
{
    public partial class mig_delete_mail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mail",
                table: "OrderHeaders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mail",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
