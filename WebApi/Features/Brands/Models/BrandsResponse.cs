using WebApi.Common.Paginations;

namespace WebApi.Features.Brands.Models;

public class BrandsResponse : PaginationResponse<BrandResponse>
{
    public BrandsResponse(List<BrandResponse> data, long total) : base(data, total)
    {
    }
}
