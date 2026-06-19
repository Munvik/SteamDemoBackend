using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Games");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(150).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(2000).IsRequired();
        builder.Property(x => x.Price).HasColumnType("numeric(10,2)").IsRequired();
        builder.Property(x => x.Rating).IsRequired();
        builder.Property(x => x.ImageUrl).HasMaxLength(500).IsRequired();

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Games)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Game { Id = 1, Title = "Elden Ring", Description = "Open-world action RPG.", Price = 59.99m, Rating = 4.8, ImageUrl = "https://example.com/images/elden-ring.jpg", CategoryId = 1 },
            new Game { Id = 2, Title = "Starfield", Description = "Space RPG adventure.", Price = 49.99m, Rating = 4.1, ImageUrl = "https://example.com/images/starfield.jpg", CategoryId = 1 },
            new Game { Id = 3, Title = "Diablo IV", Description = "Dark fantasy hack-and-slash RPG.", Price = 54.99m, Rating = 4.3, ImageUrl = "https://example.com/images/diablo4.jpg", CategoryId = 1 },
            new Game { Id = 4, Title = "DOOM Eternal", Description = "Fast-paced demon-slaying FPS.", Price = 29.99m, Rating = 4.7, ImageUrl = "https://example.com/images/doom-eternal.jpg", CategoryId = 2 },
            new Game { Id = 5, Title = "Counter-Strike 2", Description = "Competitive tactical FPS.", Price = 0.00m, Rating = 4.5, ImageUrl = "https://example.com/images/cs2.jpg", CategoryId = 2 },
            new Game { Id = 6, Title = "Halo Infinite", Description = "Sci-fi first-person shooter.", Price = 39.99m, Rating = 4.0, ImageUrl = "https://example.com/images/halo-infinite.jpg", CategoryId = 2 },
            new Game { Id = 7, Title = "Civilization VI", Description = "Turn-based grand strategy game.", Price = 24.99m, Rating = 4.6, ImageUrl = "https://example.com/images/civ6.jpg", CategoryId = 3 },
            new Game { Id = 8, Title = "Total War: Warhammer III", Description = "Epic fantasy strategy battles.", Price = 59.99m, Rating = 4.2, ImageUrl = "https://example.com/images/warhammer3.jpg", CategoryId = 3 },
            new Game { Id = 9, Title = "Age of Empires IV", Description = "Classic real-time strategy modernized.", Price = 39.99m, Rating = 4.4, ImageUrl = "https://example.com/images/aoe4.jpg", CategoryId = 3 },
            new Game { Id = 10, Title = "Hades", Description = "Rogue-like dungeon crawler.", Price = 19.99m, Rating = 4.9, ImageUrl = "https://example.com/images/hades.jpg", CategoryId = 4 },
            new Game { Id = 11, Title = "Stardew Valley", Description = "Relaxing farming and life sim.", Price = 14.99m, Rating = 4.8, ImageUrl = "https://example.com/images/stardew-valley.jpg", CategoryId = 4 },
            new Game { Id = 12, Title = "Celeste", Description = "Precision platforming indie hit.", Price = 9.99m, Rating = 4.7, ImageUrl = "https://example.com/images/celeste.jpg", CategoryId = 4 },
            new Game { Id = 13, Title = "Valheim", Description = "Viking survival sandbox.", Price = 19.99m, Rating = 4.6, ImageUrl = "https://example.com/images/valheim.jpg", CategoryId = 5 },
            new Game { Id = 14, Title = "Subnautica", Description = "Underwater survival exploration.", Price = 24.99m, Rating = 4.8, ImageUrl = "https://example.com/images/subnautica.jpg", CategoryId = 5 },
            new Game { Id = 15, Title = "The Forest", Description = "Open-world survival horror.", Price = 17.99m, Rating = 4.3, ImageUrl = "https://example.com/images/the-forest.jpg", CategoryId = 5 }
        );
    }
}
