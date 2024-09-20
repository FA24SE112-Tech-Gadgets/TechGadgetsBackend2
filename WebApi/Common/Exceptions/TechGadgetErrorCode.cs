using System.Net;

namespace WebApi.Common.Exceptions;

/**
    * Guidelines:
    *
    *   WEB: Business errors.
    *   WEV: Validation errors.
    *   WES: Server errors.
    *   WEA: Authentication/Authorization errors.
    *
    */

public class TechGadgetErrorCode
{
    public string Code { get; } = default!;
    public string Title { get; } = default!;
    public HttpStatusCode Status { get; }

    private TechGadgetErrorCode(string code, string title, HttpStatusCode status)
    {
        Code = code;
        Title = title;
        Status = status;
    }

    public static readonly TechGadgetErrorCode WEB_00 = new("WEB_00", "Lỗi không tồn tại", HttpStatusCode.BadRequest);
    public static readonly TechGadgetErrorCode WEB_01 = new("WEB_01", "Lỗi đã tồn tại", HttpStatusCode.BadRequest);
    public static readonly TechGadgetErrorCode WEB_02 = new("WEB_02", "Lỗi không hợp lệ", HttpStatusCode.BadRequest);
    public static readonly TechGadgetErrorCode WEB_03 = new("WEB_03", "Lỗi xác thực", HttpStatusCode.BadRequest);

    public static readonly TechGadgetErrorCode WEV_00 = new("WEV_00", "Lỗi cú pháp", HttpStatusCode.BadRequest);

    public static readonly TechGadgetErrorCode WES_00 = new("WES_00", "Lỗi server", HttpStatusCode.InternalServerError);

    public static readonly TechGadgetErrorCode WEA_00 = new("WEA_00", "Lỗi xác thực", HttpStatusCode.Unauthorized);
    public static readonly TechGadgetErrorCode WEA_01 = new("WEA_00", "Lỗi phân quyền", HttpStatusCode.Unauthorized);
}
