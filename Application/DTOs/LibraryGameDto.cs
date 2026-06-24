using Domain.Enums;

namespace Application.DTOs;

public record LibraryGameDto(
    int GameId,
    string Title,
    decimal Price,
    double Rating,
    DateTime PurchasedAt,
    GameStatus Status,
    string CategoryName,
    string ImageUrl);
