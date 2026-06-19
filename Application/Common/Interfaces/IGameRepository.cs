using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IGameRepository
{
    Task<List<Game>> GetAllAsync(string? search, int? categoryId, string? sortBy, CancellationToken cancellationToken);
    Task<Game?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(Game game, CancellationToken cancellationToken);
    Task UpdateAsync(Game game, CancellationToken cancellationToken);
    Task DeleteAsync(Game game, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
}
