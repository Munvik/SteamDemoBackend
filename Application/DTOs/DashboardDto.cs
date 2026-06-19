namespace Application.DTOs;

public record DashboardDto(int OwnedGames, int CompletedGames, int PlayingGames, decimal TotalSpent);
