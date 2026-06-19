using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class SteamStoreDbContext : DbContext
{
    public SteamStoreDbContext(DbContextOptions<SteamStoreDbContext> options) : base(options)
    {
    }

    public DbSet<Game> Games => Set<Game>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<User> Users => Set<User>();
    public DbSet<OwnedGame> OwnedGames => Set<OwnedGame>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SteamStoreDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
