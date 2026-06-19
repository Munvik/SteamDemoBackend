using Application.Common.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.Features.Games.Commands.DeleteGame;

public record DeleteGameCommand(int Id) : IRequest;

public class DeleteGameCommandValidator : AbstractValidator<DeleteGameCommand>
{
    public DeleteGameCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}

public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand>
{
    private readonly IGameRepository _gameRepository;

    public DeleteGameCommandHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task Handle(DeleteGameCommand request, CancellationToken cancellationToken)
    {
        var game = await _gameRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException("Game not found.");

        await _gameRepository.DeleteAsync(game, cancellationToken);
    }
}
