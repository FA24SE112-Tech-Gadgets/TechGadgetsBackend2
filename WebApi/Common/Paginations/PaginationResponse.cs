using Newtonsoft.Json;
using WebApi.Common.Exceptions;

namespace WebApi.Common.Paginations;

public abstract class PaginationResponse<T>
{
    public int Count { get; private set; }
    public int Page { get; private set; }
    public int Limit { get; private set; }
    public List<T> Data { get; private set; }

    [JsonIgnore]
    private readonly long Total;

    protected PaginationResponse(List<T> data, long total)
    {
        if (total < 0)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WES_0000)
                .AddReason("Tổng", "Lỗi khi trả về dữ liệu danh sách cho người dùng.")
                .Build();
        }

        Data = data ?? new List<T>();
        Count = Data.Count;
        Total = total;
    }

    [JsonProperty("totalPages")]
    public int TotalPages
    {
        get
        {
            if (Limit < 0)
            {
                throw TechGadgetException.NewBuilder()
                    .WithCode(TechGadgetErrorCode.WES_0000)
                    .AddReason("Tổng số trang", "Lỗi khi tính tổng trang.")
                    .Build();
            }
            return (int)((Total % Limit == 0) ? (Total / Limit) : ((Total / Limit) + 1));
        }
    }

    public void SetPage(int? page)
    {
        if (page == null || page < 0)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WES_0000)
                .AddReason("Trang", "Lỗi khi gắn biến 'page'.")
                .Build();
        }
        Page = page.Value;
    }

    public void SetLimit(int? limit)
    {
        if (limit == null || limit < 1)
        {
            throw TechGadgetException.NewBuilder()
                .WithCode(TechGadgetErrorCode.WES_0000)
                .AddReason("Giới hạn", "Lỗi khi gắn biến 'limit'.")
                .Build();
        }
        Limit = limit.Value;
    }
}
