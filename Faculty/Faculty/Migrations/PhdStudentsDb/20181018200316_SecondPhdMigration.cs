using Microsoft.EntityFrameworkCore.Migrations;

namespace Faculty.Migrations.PhdStudentsDb
{
    public partial class SecondPhdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DegreeCompletion",
                table: "PhdStudents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DegreeCompletion",
                table: "PhdStudents");
        }
    }
}
