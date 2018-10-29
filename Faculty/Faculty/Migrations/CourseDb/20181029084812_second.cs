using Microsoft.EntityFrameworkCore.Migrations;

namespace Faculty.Migrations.CourseDb
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SupervisorID = table.Column<int>(nullable: false),
                    CourseCode = table.Column<string>(nullable: true),
                    CourseTitle = table.Column<string>(nullable: true),
                    CourseSemester = table.Column<string>(nullable: true),
                    CourseYear = table.Column<string>(nullable: true),
                    Instructor = table.Column<string>(nullable: true),
                    CourseDescription = table.Column<string>(nullable: true),
                    TeachingAssistants = table.Column<string>(nullable: true),
                    LecturesTiming = table.Column<string>(nullable: true),
                    ExamInstructions = table.Column<string>(nullable: true),
                    DeadlineDay = table.Column<int>(nullable: false),
                    DeadLineMonth = table.Column<int>(nullable: false),
                    DeadlineYear = table.Column<int>(nullable: false),
                    DeadlineTime = table.Column<string>(nullable: true),
                    StudentsApproved = table.Column<string>(nullable: true),
                    StudentRegistered = table.Column<string>(nullable: true),
                    StudentDeclined = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
