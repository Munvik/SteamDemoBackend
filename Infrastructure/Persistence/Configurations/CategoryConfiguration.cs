using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

        builder.HasData(
            new Category { Id = 1, Name = "RPG" },
            new Category { Id = 2, Name = "FPS" },
            new Category { Id = 3, Name = "Strategy" },
            new Category { Id = 4, Name = "Indie" },
            new Category { Id = 5, Name = "Survival" }
        );
    }
}
