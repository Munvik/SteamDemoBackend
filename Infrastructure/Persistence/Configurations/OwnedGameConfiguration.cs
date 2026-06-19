using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class OwnedGameConfiguration : IEntityTypeConfiguration<OwnedGame>
{
    public void Configure(EntityTypeBuilder<OwnedGame> builder)
    {
        builder.ToTable("OwnedGames");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.PurchasedAt).IsRequired();
        builder.Property(x => x.Status).IsRequired();

        builder.HasIndex(x => new { x.UserId, x.GameId }).IsUnique();

        builder.HasOne(x => x.User)
            .WithMany(x => x.OwnedGames)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Game)
            .WithMany(x => x.OwnedGames)
            .HasForeignKey(x => x.GameId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
