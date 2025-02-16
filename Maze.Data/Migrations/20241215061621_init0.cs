using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maze.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AIs",
                columns: table => new
                {
                    AIId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Steps = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AIs", x => x.AIId);
                });

            migrationBuilder.CreateTable(
                name: "MazeData",
                columns: table => new
                {
                    MazeDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YLength = table.Column<int>(type: "int", nullable: false),
                    XLength = table.Column<int>(type: "int", nullable: false),
                    AIId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MazeData", x => x.MazeDataId);
                    table.ForeignKey(
                        name: "FK_MazeData_AIs_AIId",
                        column: x => x.AIId,
                        principalTable: "AIs",
                        principalColumn: "AIId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MazeData_AIId",
                table: "MazeData",
                column: "AIId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MazeData");

            migrationBuilder.DropTable(
                name: "AIs");
        }
    }
}
