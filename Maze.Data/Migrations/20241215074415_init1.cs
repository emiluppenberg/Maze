using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maze.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MazeData_AIs_AIId",
                table: "MazeData");

            migrationBuilder.DropIndex(
                name: "IX_MazeData_AIId",
                table: "MazeData");

            migrationBuilder.RenameColumn(
                name: "AIId",
                table: "MazeData",
                newName: "AIDataId");

            migrationBuilder.RenameColumn(
                name: "AIId",
                table: "AIs",
                newName: "AIDataId");

            migrationBuilder.AddColumn<int>(
                name: "MazeDataId",
                table: "AIs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MazeData_AIDataId",
                table: "MazeData",
                column: "AIDataId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MazeData_AIs_AIDataId",
                table: "MazeData",
                column: "AIDataId",
                principalTable: "AIs",
                principalColumn: "AIDataId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MazeData_AIs_AIDataId",
                table: "MazeData");

            migrationBuilder.DropIndex(
                name: "IX_MazeData_AIDataId",
                table: "MazeData");

            migrationBuilder.DropColumn(
                name: "MazeDataId",
                table: "AIs");

            migrationBuilder.RenameColumn(
                name: "AIDataId",
                table: "MazeData",
                newName: "AIId");

            migrationBuilder.RenameColumn(
                name: "AIDataId",
                table: "AIs",
                newName: "AIId");

            migrationBuilder.CreateIndex(
                name: "IX_MazeData_AIId",
                table: "MazeData",
                column: "AIId");

            migrationBuilder.AddForeignKey(
                name: "FK_MazeData_AIs_AIId",
                table: "MazeData",
                column: "AIId",
                principalTable: "AIs",
                principalColumn: "AIId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
