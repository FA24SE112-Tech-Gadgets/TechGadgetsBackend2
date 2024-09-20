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

    public static readonly TechGadgetErrorCode WEB_0000 = new("WEB_0000", "Lỗi lạ không xác định", HttpStatusCode.BadRequest);
    public static readonly TechGadgetErrorCode WEB_0001 = new("WEB_0001", "Lỗi đăng ký tài khoản", HttpStatusCode.BadRequest);
    public static readonly TechGadgetErrorCode WEB_0002 = new("WEB_0002", "Người dùng không tồn tại", HttpStatusCode.BadRequest);
    public static readonly TechGadgetErrorCode WEB_0003 = new("WEB_0003", "Tên của thương hiệu đã tồn tại", HttpStatusCode.BadRequest);

    public static readonly TechGadgetErrorCode WES_0000 = new("WES_0000", "Lỗi server", HttpStatusCode.InternalServerError);
    public static readonly TechGadgetErrorCode WES_0001 = new("WES_0001", "Lỗi trong việc tạo logo của thương hiệu", HttpStatusCode.InternalServerError);

    public static readonly TechGadgetErrorCode WEA_0000 = new("WEA_0000", "Lỗi xác thực", HttpStatusCode.Unauthorized);
    public static readonly TechGadgetErrorCode WEA_0001 = new("WEA_0001", "Người dùng chưa xác thực", HttpStatusCode.Unauthorized);
}
