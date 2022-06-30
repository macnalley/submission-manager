using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubmissionManager.Data.Migrations
{
    public partial class secondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Document",
                table: "Submissions",
                newName: "DocumentPath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocumentPath",
                table: "Submissions",
                newName: "Document");
        }
    }
}
