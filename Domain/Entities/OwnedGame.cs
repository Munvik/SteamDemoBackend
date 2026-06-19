using Domain.Enums;

namespace Domain.Entities;

public class OwnedGame
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int GameId { get; set; }
    public DateTime PurchasedAt { get; set; }
    public GameStatus Status { get; set; } = GameStatus.NotStarted;

    public User? User { get; set; }
    public Game? Game { get; set; }
}
