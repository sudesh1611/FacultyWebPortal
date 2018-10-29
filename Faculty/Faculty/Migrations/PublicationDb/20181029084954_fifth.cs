using Microsoft.EntityFrameworkCore.Migrations;

namespace Faculty.Migrations.PublicationDb
{
    public partial class fifth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PublicationTitle = table.Column<string>(nullable: true),
                    PublicationType = table.Column<string>(nullable: true),
                    PublicationLink = table.Column<string>(nullable: true),
                    PublicationYear = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Publications");
        }
    }
}
