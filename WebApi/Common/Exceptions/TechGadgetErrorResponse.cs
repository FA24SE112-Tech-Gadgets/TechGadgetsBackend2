namespace WebApi.Common.Exceptions;

public class TechGadgetErrorResponse
{
    public string Code { get; set; } = default!;
    public string Title { get; set; } = default!;
    public List<Reason> Reasons { get; set; } = [];
}

public record Reason(string Title, string Message);