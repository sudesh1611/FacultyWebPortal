using Microsoft.EntityFrameworkCore.Migrations;

namespace Faculty.Migrations.CourseResourceDb
{
    public partial class InitialResource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseID = table.Column<int>(nullable: false),
                    CourseName = table.Column<string>(nullable: true),
                    ResourceTitle = table.Column<string>(nullable: true),
                    ResourceLink = table.Column<string>(nullable: true),
                    SubmissionDirectoryLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resources");
        }
    }
}
