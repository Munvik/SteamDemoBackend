using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OwnedGameRepository : IOwnedGameRepository
{
    private readonly SteamStoreDbContext _dbContext;

    public OwnedGameRepository(SteamStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> ExistsAsync(int userId, int gameId, CancellationToken cancellationToken)
    {
        return _dbContext.OwnedGames.AnyAsync(x => x.UserId == userId && x.GameId == gameId, cancellationToken);
    }

    public async Task AddAsync(OwnedGame ownedGame, CancellationToken cancellationToken)
    {
        await _dbContext.OwnedGames.AddAsync(ownedGame, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<OwnedGame?> GetByUserAndGameIdAsync(int userId, int gameId, CancellationToken cancellationToken)
    {
        return _dbContext.OwnedGames
            .Include(x => x.Game)
            .ThenInclude(x => x!.Category)
            .FirstOrDefaultAsync(x => x.UserId == userId && x.GameId == gameId, cancellationToken);
    }

    public Task<List<OwnedGame>> GetByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return _dbContext.OwnedGames
            .AsNoTracking()
            .Include(x => x.Game)
            .ThenInclude(x => x!.Category)
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.PurchasedAt)
            .ToListAsync(cancellationToken);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
