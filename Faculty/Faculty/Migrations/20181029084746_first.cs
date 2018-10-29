using Microsoft.EntityFrameworkCore.Migrations;

namespace Faculty.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseID = table.Column<int>(nullable: false),
                    CourseName = table.Column<string>(nullable: true),
                    AssignmentTitle = table.Column<string>(nullable: true),
                    AssignmentDescription = table.Column<string>(nullable: true),
                    DeadlineDay = table.Column<int>(nullable: false),
                    DeadLineMonth = table.Column<int>(nullable: false),
                    DeadlineYear = table.Column<int>(nullable: false),
                    DeadlineTime = table.Column<string>(nullable: true),
                    AttachmentFulLink = table.Column<string>(nullable: true),
                    SubmissionDirectoryLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignments");
        }
    }
}
