namespace WebApi.Data.Entities;

public class KeywordHistory
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Keyword { get; set; } = default!;
    public DateTime CreatedAt { get; set; }

    public User? User { get; set; }
}
