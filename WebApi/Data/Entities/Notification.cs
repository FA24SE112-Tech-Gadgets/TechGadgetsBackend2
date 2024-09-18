namespace WebApi.Data.Entities;

public class Notification
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public bool IsSent { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }

    public User? User { get; set; }
}
