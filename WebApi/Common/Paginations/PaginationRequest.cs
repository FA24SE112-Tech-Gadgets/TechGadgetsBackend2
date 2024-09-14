using System.ComponentModel.DataAnnotations;

namespace WebApi.Common.Paginations;

public class PaginationRequest
{
    private const int DefaultPage = 0;
    private const int DefaultLimit = 10;

    [Range(0, int.MaxValue, ErrorMessage = "'page' must start from 0.")]
    public int Page { get; set; } = DefaultPage;

    [Range(1, int.MaxValue, ErrorMessage = "'limit' must start from 1.")]
    public int Limit { get; set; } = DefaultLimit;


    public long GetOffset()
    {
        return ((long)Page * Limit);
    }

    public override string ToString()
    {
        return $"PaginationRequest{{Page={Page}, Limit={Limit}}}";
    }
}
