using Application.Common.Interfaces;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Features.Library.Commands.UpdateOwnedGameStatus;

public record UpdateOwnedGameStatusCommand(int GameId, GameStatus Status) : IRequest;

public class UpdateOwnedGameStatusCommandValidator : AbstractValidator<UpdateOwnedGameStatusCommand>
{
    public UpdateOwnedGameStatusCommandValidator()
    {
        RuleFor(x => x.GameId).GreaterThan(0);
        RuleFor(x => x.Status).IsInEnum();
    }
}

public class UpdateOwnedGameStatusCommandHandler : IRequestHandler<UpdateOwnedGameStatusCommand>
{
    private const int FakeUserId = 1;
    private readonly IOwnedGameRepository _ownedGameRepository;

    public UpdateOwnedGameStatusCommandHandler(IOwnedGameRepository ownedGameRepository)
    {
        _ownedGameRepository = ownedGameRepository;
    }

    public async Task Handle(UpdateOwnedGameStatusCommand request, CancellationToken cancellationToken)
    {
        var ownedGame = await _ownedGameRepository.GetByUserAndGameIdAsync(FakeUserId, request.GameId, cancellationToken)
            ?? throw new KeyNotFoundException("Owned game not found.");

        ownedGame.Status = request.Status;
        await _ownedGameRepository.SaveChangesAsync(cancellationToken);
    }
}
