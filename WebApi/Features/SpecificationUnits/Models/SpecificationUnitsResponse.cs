using WebApi.Common.Paginations;

namespace WebApi.Features.SpecificationUnits.Models;

public class SpecificationUnitsResponse : PaginationResponse<SpecificationUnitResponse>
{
    public SpecificationUnitsResponse(List<SpecificationUnitResponse> data, long total) : base(data, total)
    {
    }
}
