namespace Tanner.Template.Base.API.ActionResults;

/// <summary>
/// Representa una respuesta vacía de la API.
/// </summary>
public class BaseResponse
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="traceID">Identificador de la solicitud</param>
    public BaseResponse(string? traceID)
    {
        TraceID = traceID;
    }

    /// <summary>
    /// Representa el identificador de la solicitud.
    /// </summary>
    public string? TraceID { get; }

    /// <summary>
    /// Mensaje descriptivo asociado al error.
    /// </summary>
    public string? Message { get; set; }
}

/// <summary>
/// Representa una respuesta vacía de la API.
/// </summary>
public class BaseErrorResponse : ProblemDetails
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="traceID">Identificador de la solicitud</param>
    /// <param name="statusCode">Código de la respuesta</param>
    /// <param name="message">Mensaje de la respuesta</param>
    public BaseErrorResponse(string? traceID, int statusCode, string message)
    {
        TraceID = traceID;
        Title = ReasonPhrases.GetReasonPhrase(statusCode);
        Status = statusCode;
        Type = $"https://httpstatuses.com/{statusCode}";
        Detail = message;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="traceID">Identificador de la solicitud</param>
    /// <param name="problemDetails">Problem details</param>
    public BaseErrorResponse(string? traceID, ProblemDetails problemDetails)
    {
        TraceID = traceID;
        Title = problemDetails.Title;
        Status = problemDetails.Status;
        Type = problemDetails.Type;
        Instance = problemDetails.Instance;
        Detail = problemDetails.Detail;
    }

    /// <summary>
    /// Representa el identificador de la solicitud.
    /// </summary>
    public string? TraceID { get; }

    public override string ToString() => JsonSerializer.Serialize(this);
}

/// <summary>
/// Representa una respuesta con un recurso. de la API.
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseObjectResponse<T> : BaseResponse
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="traceID"></param>
    public BaseObjectResponse(string traceID) : base(traceID)
    {
    }

    /// <summary>
    /// Recurso a devolver.
    /// </summary>
    public T Data { get; set; }
}

/// <summary>
/// Representa una respuesta con un listado de recursos.
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseListResponse<T> : BaseResponse where T : class
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="traceID"></param>
    public BaseListResponse(string traceID) : base(traceID)
    {
    }

    /// <summary>
    /// Listado de recursos a devolver.
    /// </summary>
    public List<T> Data { get; set; }

    /// <summary>
    /// Cantidad real de los recursos.
    /// </summary>
    public int Total { get; set; }
}

