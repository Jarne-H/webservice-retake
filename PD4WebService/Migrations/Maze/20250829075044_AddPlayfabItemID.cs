using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PD4ExamAPI.Migrations.Maze
{
    /// <inheritdoc />
    public partial class AddPlayfabItemID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayfabItem",
                table: "PlayfabItem");

            migrationBuilder.AlterColumn<string>(
                name: "playfabid",
                table: "PlayfabItem",
                type: "varchar(60)",
                unicode: false,
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PlayfabItemID",
                table: "PlayfabItem",
                type: "int",
                maxLength: 60,
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayfabItem",
                table: "PlayfabItem",
                column: "PlayfabItemID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayfabItem",
                table: "PlayfabItem");

            migrationBuilder.DropColumn(
                name: "PlayfabItemID",
                table: "PlayfabItem");

            migrationBuilder.AlterColumn<int>(
                name: "playfabid",
                table: "PlayfabItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldUnicode: false,
                oldMaxLength: 60);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayfabItem",
                table: "PlayfabItem",
                column: "playfabid");
        }
    }
}
