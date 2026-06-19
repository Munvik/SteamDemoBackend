namespace Application.DTOs;

public record GameDto(
    int Id,
    string Title,
    string Description,
    decimal Price,
    double Rating,
    string ImageUrl,
    int CategoryId,
    string CategoryName);
