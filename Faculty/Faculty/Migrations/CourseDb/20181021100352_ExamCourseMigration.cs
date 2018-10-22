using Microsoft.EntityFrameworkCore.Migrations;

namespace Faculty.Migrations.CourseDb
{
    public partial class ExamCourseMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExamInstructions",
                table: "Courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamInstructions",
                table: "Courses");
        }
    }
}
