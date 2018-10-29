using Microsoft.EntityFrameworkCore.Migrations;

namespace Faculty.Migrations.ProfileDb
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(nullable: false),
                    Designation = table.Column<string>(nullable: false),
                    Department = table.Column<string>(nullable: false),
                    College = table.Column<string>(nullable: false),
                    LoginEmailID = table.Column<string>(nullable: true),
                    EmailID = table.Column<string>(nullable: false),
                    MobileNumber = table.Column<string>(maxLength: 10, nullable: true),
                    GoogleScholarLink = table.Column<string>(nullable: true),
                    UnderGraduateDegreeDetails = table.Column<string>(nullable: true),
                    PostGraduateDegreeDetails = table.Column<string>(nullable: true),
                    DoctoratesDegreeDetails = table.Column<string>(nullable: true),
                    AreasOfInterest = table.Column<string>(nullable: false),
                    Achievements = table.Column<string>(nullable: true),
                    ShortBio = table.Column<string>(nullable: true),
                    ProfilePic = table.Column<string>(nullable: true),
                    ProfessionalExperience = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profiles");
        }
    }
}
