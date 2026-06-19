using Application.Common.Interfaces;
using Application.DTOs;
using Domain.Enums;
using MediatR;

namespace Application.Features.Dashboard.Queries.GetDashboard;

public record GetDashboardQuery : IRequest<DashboardDto>;

public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, DashboardDto>
{
    private const int FakeUserId = 1;
    private readonly IOwnedGameRepository _ownedGameRepository;

    public GetDashboardQueryHandler(IOwnedGameRepository ownedGameRepository)
    {
        _ownedGameRepository = ownedGameRepository;
    }

    public async Task<DashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
    {
        var ownedGames = await _ownedGameRepository.GetByUserIdAsync(FakeUserId, cancellationToken);

        var ownedCount = ownedGames.Count;
        var completedCount = ownedGames.Count(x => x.Status == GameStatus.Completed);
        var playingCount = ownedGames.Count(x => x.Status == GameStatus.Playing);
        var totalSpent = ownedGames.Sum(x => x.Game?.Price ?? 0);

        return new DashboardDto(ownedCount, completedCount, playingCount, totalSpent);
    }
}
