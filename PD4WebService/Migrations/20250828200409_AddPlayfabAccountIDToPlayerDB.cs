using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PD4ExamAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayfabAccountIDToPlayerDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MazeTile_Maze_MazeId",
                table: "MazeTile");

            migrationBuilder.RenameColumn(
                name: "MazeId",
                table: "MazeTile",
                newName: "MazeID");

            migrationBuilder.RenameIndex(
                name: "IX_MazeTile_MazeId",
                table: "MazeTile",
                newName: "IX_MazeTile_MazeID");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerID",
                table: "Player",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "PlayfabAccountID",
                table: "Player",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "TileID",
                table: "MazeTile",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<double>(
                name: "DensityFallOff",
                table: "MazeTile",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<int>(
                name: "MazeID",
                table: "Maze",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<double>(
                name: "Density",
                table: "Maze",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "OriginalMazeId",
                table: "Maze",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExternalResources",
                columns: table => new
                {
                    ResourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ResourceUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalResources", x => x.ResourceId);
                });

            migrationBuilder.CreateTable(
                name: "GameSession",
                columns: table => new
                {
                    GameSessionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MazeID = table.Column<int>(type: "int", nullable: false),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSession", x => x.GameSessionID);
                    table.ForeignKey(
                        name: "FK_GameSession_Maze",
                        column: x => x.MazeID,
                        principalTable: "Maze",
                        principalColumn: "MazeID");
                    table.ForeignKey(
                        name: "FK_GameSession_Player",
                        column: x => x.PlayerID,
                        principalTable: "Player",
                        principalColumn: "PlayerID");
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    ImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    link = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.ImageID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameSession_MazeID",
                table: "GameSession",
                column: "MazeID");

            migrationBuilder.CreateIndex(
                name: "IX_GameSession_PlayerID",
                table: "GameSession",
                column: "PlayerID");

            migrationBuilder.AddForeignKey(
                name: "FK_MazeTile_Maze",
                table: "MazeTile",
                column: "MazeID",
                principalTable: "Maze",
                principalColumn: "MazeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MazeTile_Maze",
                table: "MazeTile");

            migrationBuilder.DropTable(
                name: "ExternalResources");

            migrationBuilder.DropTable(
                name: "GameSession");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropColumn(
                name: "PlayfabAccountID",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "DensityFallOff",
                table: "MazeTile");

            migrationBuilder.DropColumn(
                name: "Density",
                table: "Maze");

            migrationBuilder.DropColumn(
                name: "OriginalMazeId",
                table: "Maze");

            migrationBuilder.RenameColumn(
                name: "MazeID",
                table: "MazeTile",
                newName: "MazeId");

            migrationBuilder.RenameIndex(
                name: "IX_MazeTile_MazeID",
                table: "MazeTile",
                newName: "IX_MazeTile_MazeId");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerID",
                table: "Player",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TileID",
                table: "MazeTile",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "MazeID",
                table: "Maze",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_MazeTile_Maze_MazeId",
                table: "MazeTile",
                column: "MazeId",
                principalTable: "Maze",
                principalColumn: "MazeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
