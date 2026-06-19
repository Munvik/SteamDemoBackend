using Domain.Entities;
using Domain.Enums;

namespace Application.Common.Interfaces;

public interface IOwnedGameRepository
{
    Task<bool> ExistsAsync(int userId, int gameId, CancellationToken cancellationToken);
    Task AddAsync(OwnedGame ownedGame, CancellationToken cancellationToken);
    Task<OwnedGame?> GetByUserAndGameIdAsync(int userId, int gameId, CancellationToken cancellationToken);
    Task<List<OwnedGame>> GetByUserIdAsync(int userId, CancellationToken cancellationToken);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
