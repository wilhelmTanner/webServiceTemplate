namespace Tanner.Template.Base.API.Extensions;

/// <summary>
/// Clase que permite la configuración del middleware que maneja las excepciones.
/// </summary>
public static class ExceptionMiddlewareExtensions
{
    /// <summary>
    /// Este método permite configurar el middleware que maneja las excepciones.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="loggerFactory"></param>
    public static void ConfigureExceptionHandler(this WebApplication app, ILoggerFactory loggerFactory)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    var logger = loggerFactory.CreateLogger<ExceptionHandlerMiddleware>();
                    logger.LogError(new EventId(contextFeature.Error.HResult),
                        contextFeature.Error, "{Message}",
                        contextFeature.Error.Message);

                    var details = new ProblemDetails();
                    bool isDevelopment = app.Environment.IsDevelopment();

                    switch (contextFeature.Error)
                    {
                        case BaseException:
                            details.Status = StatusCodes.Status400BadRequest;
                            details.Title = contextFeature.Error.Message;
                            break;
                        default:
                            details.Status = StatusCodes.Status500InternalServerError;
                            details.Title = isDevelopment ?
                                contextFeature.Error.Message :
                                "Ha ocurrido un error al procesar su solicitud.";
                            break;
                    }

                    details.Type = isDevelopment ?
                        contextFeature.Error.GetType().Name :
                        "https://tools.ietf.org/html/rfc7231#section-6.6.1";

                    details.Detail = isDevelopment ?
                        contextFeature.Error.StackTrace :
                        null;

                    details.Instance = context.Request.Path;

                    var result = new BaseErrorResponse(context.TraceIdentifier, details).ToString();

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)details.Status;
                    await context.Response.WriteAsync(result);
                }
            });
        });
    }
}
