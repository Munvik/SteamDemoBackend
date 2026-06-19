using Application.Common.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Games.Commands.UpdateGame;

public record UpdateGameCommand(
    int Id,
    string Title,
    string Description,
    decimal Price,
    double Rating,
    string ImageUrl,
    int CategoryId) : IRequest;

public class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
{
    public UpdateGameCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Rating).InclusiveBetween(0, 5);
        RuleFor(x => x.ImageUrl).NotEmpty().MaximumLength(500);
        RuleFor(x => x.CategoryId).GreaterThan(0);
    }
}

public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand>
{
    private readonly IGameRepository _gameRepository;
    private readonly ICategoryRepository _categoryRepository;

    public UpdateGameCommandHandler(IGameRepository gameRepository, ICategoryRepository categoryRepository)
    {
        _gameRepository = gameRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task Handle(UpdateGameCommand request, CancellationToken cancellationToken)
    {
        var game = await _gameRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException("Game not found.");

        var categoryExists = await _categoryRepository.ExistsAsync(request.CategoryId, cancellationToken);
        if (!categoryExists)
        {
            throw new InvalidOperationException("Category not found.");
        }

        game.Title = request.Title;
        game.Description = request.Description;
        game.Price = request.Price;
        game.Rating = request.Rating;
        game.ImageUrl = request.ImageUrl;
        game.CategoryId = request.CategoryId;

        await _gameRepository.UpdateAsync(game, cancellationToken);
    }
}
