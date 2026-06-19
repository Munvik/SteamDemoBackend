using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Features.Store.Commands.BuyGame;

public record BuyGameCommand(int GameId) : IRequest<int>;

public class BuyGameCommandValidator : AbstractValidator<BuyGameCommand>
{
    public BuyGameCommandValidator()
    {
        RuleFor(x => x.GameId).GreaterThan(0);
    }
}

public class BuyGameCommandHandler : IRequestHandler<BuyGameCommand, int>
{
    private const int FakeUserId = 1;

    private readonly IGameRepository _gameRepository;
    private readonly IOwnedGameRepository _ownedGameRepository;
    private readonly IUserRepository _userRepository;

    public BuyGameCommandHandler(
        IGameRepository gameRepository,
        IOwnedGameRepository ownedGameRepository,
        IUserRepository userRepository)
    {
        _gameRepository = gameRepository;
        _ownedGameRepository = ownedGameRepository;
        _userRepository = userRepository;
    }

    public async Task<int> Handle(BuyGameCommand request, CancellationToken cancellationToken)
    {
        var gameExists = await _gameRepository.ExistsAsync(request.GameId, cancellationToken);
        if (!gameExists)
        {
            throw new KeyNotFoundException("Game not found.");
        }

        var user = await _userRepository.GetByIdAsync(FakeUserId, cancellationToken)
            ?? throw new KeyNotFoundException("User not found.");

        var alreadyOwned = await _ownedGameRepository.ExistsAsync(user.Id, request.GameId, cancellationToken);
        if (alreadyOwned)
        {
            throw new InvalidOperationException("Game is already in library.");
        }

        var ownedGame = new OwnedGame
        {
            UserId = user.Id,
            GameId = request.GameId,
            PurchasedAt = DateTime.UtcNow,
            Status = GameStatus.NotStarted
        };

        await _ownedGameRepository.AddAsync(ownedGame, cancellationToken);
        return ownedGame.Id;
    }
}
