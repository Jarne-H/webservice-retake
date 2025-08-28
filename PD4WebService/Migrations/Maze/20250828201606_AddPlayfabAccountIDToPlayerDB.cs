using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PD4ExamAPI.Migrations.Maze
{
    /// <inheritdoc />
    public partial class AddPlayfabAccountIDToPlayerDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
    name: "PlayfabAccountID",
    table: "Player",
    type: "nvarchar(max)",
    nullable: false,
    defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OriginalMazeID",
                table: "Maze",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
    name: "PlayfabAccountID",
    table: "Player");

            migrationBuilder.DropColumn(
                name: "OriginalMazeID",
                table: "Maze");
        }
    }
}
