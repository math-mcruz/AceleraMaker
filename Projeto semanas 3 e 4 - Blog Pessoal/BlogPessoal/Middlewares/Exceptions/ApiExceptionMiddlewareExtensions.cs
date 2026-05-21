using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace BlogPessoal.Middlewares.Exceptions;
public static class ApiExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (contextFeature != null)
                {
                    var exception = contextFeature.Error;

                    context.Response.StatusCode = exception switch
                    {
                        KeyNotFoundException => (int)HttpStatusCode.NotFound,           
                        ArgumentException => (int)HttpStatusCode.BadRequest,             
                        InvalidOperationException => (int)HttpStatusCode.Conflict,       
                        UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized, 
                        _ => (int)HttpStatusCode.InternalServerError                     
                    };

                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = exception.Message, 
                        Trace = contextFeature.Error.StackTrace 
                    }.ToString());
                }
            });
        });
    }
}