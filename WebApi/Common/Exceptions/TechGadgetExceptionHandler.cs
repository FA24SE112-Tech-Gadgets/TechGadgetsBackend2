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
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = new
        {
            Message = "An unexpected error occurred.",
            Detail = ex.Message
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}
