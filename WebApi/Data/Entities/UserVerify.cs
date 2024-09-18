namespace WebApi.Data.Entities;

public class UserVerify
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string VerifyCode { get; set; } = default!;
    public VerifyStatus Status;

    public User? User { get; set; }
}

public enum VerifyStatus
{
    Pending,
    Verified,
    Expired
}