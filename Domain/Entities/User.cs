namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;

    public ICollection<OwnedGame> OwnedGames { get; set; } = new List<OwnedGame>();
}
