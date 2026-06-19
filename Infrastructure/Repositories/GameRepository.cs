using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GameRepository : IGameRepository
{
    private readonly SteamStoreDbContext _dbContext;

    public GameRepository(SteamStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Game>> GetAllAsync(string? search, int? categoryId, string? sortBy, CancellationToken cancellationToken)
    {
        var query = _dbContext.Games
            .AsNoTracking()
            .Include(x => x.Category)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var lower = search.Trim().ToLower();
            query = query.Where(x => x.Title.ToLower().Contains(lower) || x.Description.ToLower().Contains(lower));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(x => x.CategoryId == categoryId.Value);
        }

        query = sortBy?.Trim().ToLower() switch
        {
            "price" => query.OrderBy(x => x.Price),
            "rating" => query.OrderByDescending(x => x.Rating),
            "title" => query.OrderBy(x => x.Title),
            _ => query.OrderBy(x => x.Id)
        };

        return await query.ToListAsync(cancellationToken);
    }

    public Task<Game?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _dbContext.Games
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(Game game, CancellationToken cancellationToken)
    {
        await _dbContext.Games.AddAsync(game, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Game game, CancellationToken cancellationToken)
    {
        _dbContext.Games.Update(game);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Game game, CancellationToken cancellationToken)
    {
        _dbContext.Games.Remove(game);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        return _dbContext.Games.AnyAsync(x => x.Id == id, cancellationToken);
    }
}
