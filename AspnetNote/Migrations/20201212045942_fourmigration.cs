using Microsoft.EntityFrameworkCore.Migrations;

namespace AspnetNote.Migrations
{
    public partial class fourmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoteContent5",
                table: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "NoteContent_sample",
                table: "Notes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoteContent_sample",
                table: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "NoteContent5",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
