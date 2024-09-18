namespace WebApi.Common.Utils;

public static class VerificationCodeGenerator
{
    private static readonly Random Random = new Random();
    private const int MaxCodeValue = 1000000;

    public static string Generate()
    {
        var number = Random.Next(MaxCodeValue);
        return number.ToString("D6");
    }
}
