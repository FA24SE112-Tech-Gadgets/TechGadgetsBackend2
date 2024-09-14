//using Newtonsoft.Json;

//namespace WebApi.Common.Paginations;

//public abstract class PaginationResponse<T>
//{
//    public int Count { get; private set; }
//    public int Page { get; private set; }
//    public int Limit { get; private set; }
//    public List<T> Data { get; private set; }

//    [JsonIgnore]
//    private readonly long Total;

//    protected PaginationResponse(List<T> data, long total)
//    {
//        if (total < 0)
//        {
//            throw ProjectException.NewBuilder()
//                .WithCode(ProjectErrorCode.WES_0003)
//                .AddReason("total", "Error while returning the list data to the user.")
//                .Build();
//        }

//        Data = data ?? new List<T>();
//        Count = Data.Count;
//        Total = total;
//    }

//    [JsonProperty("totalPages")]
//    public int TotalPages
//    {
//        get
//        {
//            if (Limit < 0)
//            {
//                throw ProjectException.NewBuilder()
//                    .WithCode(ProjectErrorCode.WES_0003)
//                    .AddReason("totalPages", "Error calculating total pages.")
//                    .Build();
//            }
//            return (int)((Total % Limit == 0) ? (Total / Limit) : ((Total / Limit) + 1));
//        }
//    }

//    public void SetPage(int? page)
//    {
//        if (page == null || page < 0)
//        {
//            throw ProjectException.NewBuilder()
//                .WithCode(ProjectErrorCode.WES_0003)
//                .AddReason("page", "Error setting the 'page' variable.")
//                .Build();
//        }
//        Page = page.Value;
//    }

//    public void SetLimit(int? limit)
//    {
//        if (limit == null || limit < 1)
//        {
//            throw ProjectException.NewBuilder()
//                .WithCode(ProjectErrorCode.WES_0003)
//                .AddReason("limit", "Error setting the 'limit' variable.")
//                .Build();
//        }
//        Limit = limit.Value;
//    }
//}
