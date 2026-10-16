using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BilliardsManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BreakPotSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartingPot = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TicketSalesRevenue = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    DrawerPlayerId = table.Column<int>(type: "INTEGER", nullable: true),
                    BallsMade = table.Column<int>(type: "INTEGER", nullable: false),
                    PayoutAmount = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    EndingPot = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakPotSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BreakPotSessions_Players_DrawerPlayerId",
                        column: x => x.DrawerPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FirstPlaceId = table.Column<int>(type: "INTEGER", nullable: true),
                    SecondPlaceId = table.Column<int>(type: "INTEGER", nullable: true),
                    ThirdPlaceId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tournaments_Players_FirstPlaceId",
                        column: x => x.FirstPlaceId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tournaments_Players_SecondPlaceId",
                        column: x => x.SecondPlaceId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tournaments_Players_ThirdPlaceId",
                        column: x => x.ThirdPlaceId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TicketPurchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BreakPotSessionId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    TierAmount = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TicketCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPurchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketPurchases_BreakPotSessions_BreakPotSessionId",
                        column: x => x.BreakPotSessionId,
                        principalTable: "BreakPotSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketPurchases_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BreakPotSessions_DrawerPlayerId",
                table: "BreakPotSessions",
                column: "DrawerPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketPurchases_BreakPotSessionId",
                table: "TicketPurchases",
                column: "BreakPotSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketPurchases_PlayerId",
                table: "TicketPurchases",
                column: "PlayerId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketPurchases");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "BreakPotSessions");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
