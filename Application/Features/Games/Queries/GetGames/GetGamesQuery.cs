using System.Text.Json;
using Application.Common;
using Application.Common.Interfaces;
using Application.DTOs;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Features.Games.Queries.GetGames;

public record GetGamesQuery(string? Search, int? CategoryId, string? SortBy) : IRequest<IReadOnlyList<GameDto>>;

public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, IReadOnlyList<GameDto>>
{
    private readonly IGameRepository _gameRepository;
    private readonly IDistributedCache _cache;

    public GetGamesQueryHandler(IGameRepository gameRepository, IDistributedCache cache)
    {
        _gameRepository = gameRepository;
        _cache = cache;
    }

    public async Task<IReadOnlyList<GameDto>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        List<Game> games = new();

        if (!string.IsNullOrWhiteSpace(request.Search) ||
            request.CategoryId is not null ||
            request.SortBy is not null)
        {
            games = await _gameRepository.GetAllAsync(
                request.Search,
                request.CategoryId,
                request.SortBy,
                cancellationToken);

            return games.Select(g => new GameDto(
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

        var gamesCache = await _cache.GetStringAsync(CacheKeys.Games, cancellationToken);
        List<GameDto> gamesDto = new();

        if (!string.IsNullOrEmpty(gamesCache))
        {
            gamesDto = JsonSerializer.Deserialize<List<GameDto>>(gamesCache) ?? new List<GameDto>();
        }
        else
        {
            games = await _gameRepository.GetAllAsync(request.Search, request.CategoryId, request.SortBy, cancellationToken);
            gamesDto = games
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

            await _cache.SetStringAsync(CacheKeys.Games, JsonSerializer.Serialize(gamesDto),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                }, cancellationToken);
        }

        return gamesDto;
    }
}
