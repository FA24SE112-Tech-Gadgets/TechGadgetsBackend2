using WebApi.Common.Paginations;

namespace WebApi.Features.BusinessModels.Models;

public class BusinessModelsResponse : PaginationResponse<BusinessModelResponse>
{
    public BusinessModelsResponse(List<BusinessModelResponse> data, long total) : base(data, total)
    {
    }
}
