using Microsoft.EntityFrameworkCore.Migrations;

namespace Faculty.Migrations.PhdStudentsDb
{
    public partial class InitialPhdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhdStudents",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SupervisorID = table.Column<int>(nullable: false),
                    StudentName = table.Column<string>(nullable: true),
                    ThesisTitle = table.Column<string>(nullable: true),
                    DegreeStatus = table.Column<string>(nullable: true),
                    College = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhdStudents", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhdStudents");
        }
    }
}
