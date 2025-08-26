using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PD4ExamAPI.Migrations.Maze
{
    /// <inheritdoc />
    public partial class StartOfExam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    ImageID = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    link = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.ImageID);
                });

            migrationBuilder.CreateTable(
                name: "Maze",
                columns: table => new
                {
                    MazeID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    CreationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Density = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maze", x => x.MazeID);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    CreationDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerID);
                });

            migrationBuilder.CreateTable(
                name: "MazeTile",
                columns: table => new
                {
                    TileID = table.Column<int>(type: "int", nullable: false),
                    RowIndex = table.Column<int>(type: "int", nullable: false),
                    ColumnIndex = table.Column<int>(type: "int", nullable: false),
                    TileType = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    MazeID = table.Column<int>(type: "int", nullable: false),
                    DensityFallOff = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MazeTile", x => x.TileID);
                    table.ForeignKey(
                        name: "FK_MazeTile_Maze",
                        column: x => x.MazeID,
                        principalTable: "Maze",
                        principalColumn: "MazeID");
                });

            migrationBuilder.CreateTable(
                name: "GameSession",
                columns: table => new
                {
                    GameSessionID = table.Column<int>(type: "int", nullable: false),
                    MazeID = table.Column<int>(type: "int", nullable: false),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSession", x => x.GameSessionID);
                    table.ForeignKey(
                        name: "FK_GameSession_GameSession",
                        column: x => x.MazeID,
                        principalTable: "Maze",
                        principalColumn: "MazeID");
                    table.ForeignKey(
                        name: "FK_GameSession_Player",
                        column: x => x.PlayerID,
                        principalTable: "Player",
                        principalColumn: "PlayerID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameSession_MazeID",
                table: "GameSession",
                column: "MazeID");

            migrationBuilder.CreateIndex(
                name: "IX_GameSession_PlayerID",
                table: "GameSession",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_MazeTile_MazeID",
                table: "MazeTile",
                column: "MazeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameSession");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "MazeTile");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Maze");
        }
    }
}
