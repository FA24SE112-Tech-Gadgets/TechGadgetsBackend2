namespace WebApi.Common.Exceptions;

public class TechGadgetExceptionHandler(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await Handle(ex, context);
        }
    }

    private static async Task Handle(Exception ex, HttpContext context)
    {
        if (ex is TechGadgetException techGadgetException)
        {
            var errorResponse = new TechGadgetErrorResponse
            {
                Code = techGadgetException.ErrorCode.Code,
                Title = techGadgetException.ErrorCode.Title,
                Reasons = techGadgetException.GetReasons().Select(reason => new Reason(reason.Title, reason.ReasonMessage)).ToList()
            };

            context.Response.StatusCode = (int)techGadgetException.ErrorCode.Status;
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
