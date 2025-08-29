using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PD4ExamAPI.Migrations.Maze
{
    /// <inheritdoc />
    public partial class AddPlayfabItemToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayfabItem",
                columns: table => new
                {
                    playfabid = table.Column<int>(type: "int", nullable: false),
                    displayname = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayfabItem", x => x.playfabid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayfabItem");
        }
    }
}
