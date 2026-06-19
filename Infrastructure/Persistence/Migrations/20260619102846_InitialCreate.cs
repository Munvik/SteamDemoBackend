using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Rating = table.Column<double>(type: "double precision", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OwnedGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    PurchasedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnedGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnedGames_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OwnedGames_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "RPG" },
                    { 2, "FPS" },
                    { 3, "Strategy" },
                    { 4, "Indie" },
                    { 5, "Survival" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Username" },
                values: new object[] { 1, "demo-user" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Price", "Rating", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Open-world action RPG.", "https://example.com/images/elden-ring.jpg", 59.99m, 4.7999999999999998, "Elden Ring" },
                    { 2, 1, "Space RPG adventure.", "https://example.com/images/starfield.jpg", 49.99m, 4.0999999999999996, "Starfield" },
                    { 3, 1, "Dark fantasy hack-and-slash RPG.", "https://example.com/images/diablo4.jpg", 54.99m, 4.2999999999999998, "Diablo IV" },
                    { 4, 2, "Fast-paced demon-slaying FPS.", "https://example.com/images/doom-eternal.jpg", 29.99m, 4.7000000000000002, "DOOM Eternal" },
                    { 5, 2, "Competitive tactical FPS.", "https://example.com/images/cs2.jpg", 0.00m, 4.5, "Counter-Strike 2" },
                    { 6, 2, "Sci-fi first-person shooter.", "https://example.com/images/halo-infinite.jpg", 39.99m, 4.0, "Halo Infinite" },
                    { 7, 3, "Turn-based grand strategy game.", "https://example.com/images/civ6.jpg", 24.99m, 4.5999999999999996, "Civilization VI" },
                    { 8, 3, "Epic fantasy strategy battles.", "https://example.com/images/warhammer3.jpg", 59.99m, 4.2000000000000002, "Total War: Warhammer III" },
                    { 9, 3, "Classic real-time strategy modernized.", "https://example.com/images/aoe4.jpg", 39.99m, 4.4000000000000004, "Age of Empires IV" },
                    { 10, 4, "Rogue-like dungeon crawler.", "https://example.com/images/hades.jpg", 19.99m, 4.9000000000000004, "Hades" },
                    { 11, 4, "Relaxing farming and life sim.", "https://example.com/images/stardew-valley.jpg", 14.99m, 4.7999999999999998, "Stardew Valley" },
                    { 12, 4, "Precision platforming indie hit.", "https://example.com/images/celeste.jpg", 9.99m, 4.7000000000000002, "Celeste" },
                    { 13, 5, "Viking survival sandbox.", "https://example.com/images/valheim.jpg", 19.99m, 4.5999999999999996, "Valheim" },
                    { 14, 5, "Underwater survival exploration.", "https://example.com/images/subnautica.jpg", 24.99m, 4.7999999999999998, "Subnautica" },
                    { 15, 5, "Open-world survival horror.", "https://example.com/images/the-forest.jpg", 17.99m, 4.2999999999999998, "The Forest" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_CategoryId",
                table: "Games",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnedGames_GameId",
                table: "OwnedGames",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnedGames_UserId_GameId",
                table: "OwnedGames",
                columns: new[] { "UserId", "GameId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnedGames");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
