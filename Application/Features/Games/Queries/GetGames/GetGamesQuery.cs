using Application.Common.Interfaces;
using Application.DTOs;
using MediatR;

namespace Application.Features.Games.Queries.GetGames;

public record GetGamesQuery(string? Search, int? CategoryId, string? SortBy) : IRequest<IReadOnlyList<GameDto>>;

public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, IReadOnlyList<GameDto>>
{
    private readonly IGameRepository _gameRepository;

    public GetGamesQueryHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<IReadOnlyList<GameDto>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        var games = await _gameRepository.GetAllAsync(request.Search, request.CategoryId, request.SortBy, cancellationToken);
        return games
            .Select(g => new GameDto(
                g.Id,
                g.Title,
                g.Description,
                g.Price,
                g.Rating,
                g.ImageUrl,
                g.CategoryId,
                g.Category?.Name ?? string.Empty))
            .ToList();
    }
}
