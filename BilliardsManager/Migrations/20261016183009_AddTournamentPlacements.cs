using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilliardsManager.Migrations
{
    /// <inheritdoc />
    public partial class AddTournamentPlacements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Players_FirstPlaceId",
                table: "Tournaments");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Players_SecondPlaceId",
                table: "Tournaments");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournaments_Players_ThirdPlaceId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_FirstPlaceId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_SecondPlaceId",
                table: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Tournaments_ThirdPlaceId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "FirstPlaceId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "SecondPlaceId",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "ThirdPlaceId",
                table: "Tournaments");

            migrationBuilder.CreateTable(
                name: "TournamentPlacement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TournamentId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    AwardType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentPlacement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentPlacement_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentPlacement_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TournamentPlacement_PlayerId",
                table: "TournamentPlacement",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentPlacement_TournamentId",
                table: "TournamentPlacement",
                column: "TournamentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TournamentPlacement");

            migrationBuilder.AddColumn<int>(
                name: "FirstPlaceId",
                table: "Tournaments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondPlaceId",
                table: "Tournaments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThirdPlaceId",
                table: "Tournaments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_FirstPlaceId",
                table: "Tournaments",
                column: "FirstPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_SecondPlaceId",
                table: "Tournaments",
                column: "SecondPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_ThirdPlaceId",
                table: "Tournaments",
                column: "ThirdPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Players_FirstPlaceId",
                table: "Tournaments",
                column: "FirstPlaceId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Players_SecondPlaceId",
                table: "Tournaments",
                column: "SecondPlaceId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournaments_Players_ThirdPlaceId",
                table: "Tournaments",
                column: "ThirdPlaceId",
                principalTable: "Players",
                principalColumn: "Id");
        }
    }
}
