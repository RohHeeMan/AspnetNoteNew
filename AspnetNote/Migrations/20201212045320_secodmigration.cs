using Microsoft.EntityFrameworkCore.Migrations;

namespace AspnetNote.Migrations
{
    public partial class secodmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NoteContent2",
                table: "Notes",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoteContent2",
                table: "Notes");
        }
    }
}
