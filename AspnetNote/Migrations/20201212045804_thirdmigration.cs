using Microsoft.EntityFrameworkCore.Migrations;

namespace AspnetNote.Migrations
{
    public partial class thirdmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoteContent2",
                table: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "NoteContent5",
                table: "Notes",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoteContent5",
                table: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "NoteContent2",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
