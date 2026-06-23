using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGamesLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                UPDATE "Games"
                SET "ImageUrl" =
                    CASE "Id"
                        WHEN 1 THEN 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1245620/capsule_616x353.jpg'
                        WHEN 2 THEN 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1716740/capsule_616x353.jpg'
                        WHEN 3 THEN 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/2344520/capsule_616x353.jpg'
                        WHEN 4 THEN 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/782330/capsule_616x353.jpg'
                        WHEN 5 THEN 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/730/capsule_616x353.jpg'
                        WHEN 6 THEN 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1240440/capsule_616x353.jpg'
                        WHEN 7 THEN 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/289070/capsule_616x353.jpg'
                        WHEN 8 THEN 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1142710/capsule_616x353.jpg'
                        WHEN 9 THEN 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1466860/capsule_616x353.jpg'
                        WHEN 10 THEN 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1145360/capsule_616x353.jpg'
                        WHEN 11 THEN 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/413150/capsule_616x353.jpg'
                        WHEN 12 THEN 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/504230/capsule_616x353.jpg'
                        WHEN 13 THEN 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/892970/capsule_616x353.jpg'
                        WHEN 14 THEN 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/264710/capsule_616x353.jpg'
                        WHEN 15 THEN 'https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/242760/capsule_616x353.jpg'
                    END
                WHERE "Id" BETWEEN 1 AND 15;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
