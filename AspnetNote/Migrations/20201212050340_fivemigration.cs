using Microsoft.EntityFrameworkCore.Migrations;

namespace AspnetNote.Migrations
{
    public partial class fivemigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoteContent_sample",
                table: "Notes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NoteContent_sample",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
