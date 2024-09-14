﻿using System.Net;

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
    public string Title { get; } = default!;
    public HttpStatusCode Status { get; }

    private TechGadgetErrorCode(string title, HttpStatusCode status)
    {
        Title = title;
        Status = status;
    }

    public static readonly TechGadgetErrorCode WEB_0000 = new("Dummy business error code", HttpStatusCode.BadRequest);
    public static readonly TechGadgetErrorCode WEB_0001 = new("Restaurant name or email already exists", HttpStatusCode.BadRequest);
    public static readonly TechGadgetErrorCode WEB_0002 = new("Nhà hàng không tồn tại", HttpStatusCode.BadRequest);
    public static readonly TechGadgetErrorCode WEB_0003 = new("Invalid restaurant status", HttpStatusCode.BadRequest);
    public static readonly TechGadgetErrorCode WEB_0004 = new("Tên của món ăn đã bị trùng", HttpStatusCode.BadRequest);
}
