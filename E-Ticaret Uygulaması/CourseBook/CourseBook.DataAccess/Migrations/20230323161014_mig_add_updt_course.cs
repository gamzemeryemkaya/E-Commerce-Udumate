using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseBook.DataAccess.Migrations
{
    public partial class mig_add_updt_course : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ProductId",
                table: "Courses",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Products_ProductId",
                table: "Courses",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Products_ProductId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ProductId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Courses");
        }
    }
}
