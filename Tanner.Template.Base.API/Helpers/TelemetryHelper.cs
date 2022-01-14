namespace Tanner.Template.Base.API.Helpers;

public static class TelemetryHelper
{
    private const string TraceIdentifierKey = "traceId";

    /// <summary>
    /// Modifica el trace Id del <see cref="ProblemDetails"/> teniendo como referencia el <see href="https://tools.ietf.org/html/">RFC7807</see>.
    /// Para Application Insights formato '00-OperationId-RequestId-00'.
    /// </summary>
    /// <param name="actionContext"></param>
    /// <param name="problemDetails"></param>
    public static void SetTraceIdToProblemDetails(ActionContext actionContext, ProblemDetails problemDetails)
    {
        string traceId = Activity.Current?.Id ?? actionContext.HttpContext.TraceIdentifier;
        problemDetails.Extensions[TraceIdentifierKey] = traceId;
    }

    /// <summary>
    /// Retorna el identificador de la traza.
    /// Para Application Insights formato '00-OperationId-RequestId-00'.
    /// </summary>
    /// <param name="httpContext"></param>
    public static string GetTraceId(HttpContext? httpContext) => Activity.Current?.Id ?? httpContext?.TraceIdentifier ?? string.Empty;
}