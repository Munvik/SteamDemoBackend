using Application.Common.Interfaces;
using FluentValidation;
using MediatR;
using Domain.Entities;

namespace Application.Features.Games.Commands.CreateGame;

public record CreateGameCommand(
    string Title,
    string Description,
    decimal Price,
    double Rating,
    string ImageUrl,
    int CategoryId) : IRequest<int>;

public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
{
    public CreateGameCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(150);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Rating).InclusiveBetween(0, 5);
        RuleFor(x => x.ImageUrl).NotEmpty().MaximumLength(500);
        RuleFor(x => x.CategoryId).GreaterThan(0);
    }
}

public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, int>
{
    private readonly IGameRepository _gameRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CreateGameCommandHandler(IGameRepository gameRepository, ICategoryRepository categoryRepository)
    {
        _gameRepository = gameRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<int> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        var categoryExists = await _categoryRepository.ExistsAsync(request.CategoryId, cancellationToken);
        if (!categoryExists)
        {
            throw new InvalidOperationException("Category not found.");
        }

        var game = new Game
        {
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            Rating = request.Rating,
            ImageUrl = request.ImageUrl,
            CategoryId = request.CategoryId
        };

        await _gameRepository.AddAsync(game, cancellationToken);
        return game.Id;
    }
}
