using Application.Common.Interfaces;
using Application.DTOs;
using MediatR;

namespace Application.Features.Games.Queries.GetGameById;

public record GetGameByIdQuery(int Id) : IRequest<GameDto>;

public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, GameDto>
{
    private readonly IGameRepository _gameRepository;

    public GetGameByIdQueryHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<GameDto> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
    {
        var game = await _gameRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException("Game not found.");

        return new GameDto(
            game.Id,
            game.Title,
            game.Description,
            game.Price,
            game.Rating,
            game.ImageUrl,
            game.CategoryId,
            game.Category?.Name ?? string.Empty);
    }
}
