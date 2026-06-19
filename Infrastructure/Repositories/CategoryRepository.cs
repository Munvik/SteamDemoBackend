using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly SteamStoreDbContext _dbContext;

    public CategoryRepository(SteamStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _dbContext.Categories
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        return _dbContext.Categories.AnyAsync(x => x.Id == id, cancellationToken);
    }
}
