namespace Tanner.Template.Base.API.Controllers;

/// <summary>
/// Controlador BASE
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CustomControllerBase : ControllerBase
{
    /// <summary>
    /// Representa una respuesta OK que contiene un recurso.
    /// </summary>
    /// <param name="message">Mensaje asociado a la respuesta</param>
    /// <returns>200 con recurso</returns>
    [NonAction]
    protected ActionResult<BaseResponse> CustomOk(string? message = null)
    {
        string traceID = HttpContext.TraceIdentifier;

        var result = new BaseResponse(traceID)
        {
            Message = message
        };

        return Ok(result);
    }

    /// <summary>
    /// Representa una respuesta OK que contiene un recurso.
    /// </summary>
    /// <typeparam name="T">Tipo del recurso a devolver</typeparam>
    /// <param name="element">Recurso</param>
    /// <param name="message">Mensaje asociado a la respuesta</param>
    /// <returns>200 con recurso</returns>
    [NonAction]
    protected ActionResult<BaseObjectResponse<T>> CustomOk<T>(T element, string? message = null)
    {
        string traceID = HttpContext.TraceIdentifier;

        if (element == null)
        {
            return CustomNotFound();
        }

        var result = new BaseObjectResponse<T>(traceID)
        {
            Message = message,
            Data = element
        };

        return Ok(result);
    }

    /// <summary>
    /// Representa una respuesta OK que contiene un listado de recursos.
    /// </summary>
    /// <typeparam name="T">Tipo de los recursos a devolver</typeparam>
    /// <param name="elements">Recursos</param>
    /// <param name="total">Total real de rescursos</param>
    /// <param name="message">Mensaje asociado a la respuesta</param>
    /// <returns>200 con recursos</returns>
    [NonAction]
    protected ActionResult<BaseListResponse<T>> CustomOk<T>(List<T> elements, int total, string? message = null)
        where T : class
    {
        string traceID = HttpContext.TraceIdentifier;
        var result = new BaseListResponse<T>(traceID)
        {
            Message = message,
            Data = elements,
            Total = total
        };

        return Ok(result);
    }

    /// <summary>
    /// Representa una respuesta OK sin datos.
    /// Devol
    /// </summary>
    /// <param name="routeName">Nombre de la ruta para el Location del Header.</param>
    /// <param name="element">Recurso creado</param>
    /// <param name="routeValues">Elementos de la ruta para el Get</param>
    /// <param name="message">Mensaje asociado al resultado</param>
    /// <returns>201 con recurso</returns>
    [NonAction]
    protected ActionResult<BaseObjectResponse<T>> CustomCreate<T>(string routeName, T element, object routeValues = null, string? message = null)
        where T : class
    {
        string traceID = HttpContext.TraceIdentifier;
        var result = new BaseObjectResponse<T>(traceID)
        {
            Message = message,
            Data = element
        };

        return CreatedAtRoute(routeName, routeValues, result);
    }

    /// <summary>
    /// Representa una respuesta BadResquest para declarar un NotFound.
    /// </summary>
    /// <param name="message">Mensaje asociado a la respuesta</param>
    /// <returns>400 con código 404</returns>
    [NonAction]
    protected ActionResult CustomNotFound(string? message = null)
    {
        string traceID = HttpContext.TraceIdentifier;
        int statusCode = (int)HttpStatusCode.NotFound;
        message ??= "No se pudo encontrar el recurso";
        var result = new BaseErrorResponse(traceID, statusCode, message)
        {
            Instance = HttpContext.Request.Path
        };

        return BadRequest(result);
    }

    /// <summary>
    /// Representa una respuesta Conflict.
    /// </summary>
    /// <param name="message">Mensaje asociado a la respuesta.</param>
    /// <returns>409</returns>
    [NonAction]
    protected ActionResult CustomConflict(string message)
    {
        string traceID = HttpContext.TraceIdentifier;
        int statusCode = (int)HttpStatusCode.Conflict;
        var result = new BaseErrorResponse(traceID, statusCode, message)
        {
            Instance = HttpContext.Request.Path
        };

        return Conflict(result);
    }

    /// <summary>
    /// Representa una respuesta BadRequest.
    /// </summary>
    /// <param name="message">Mensaje asociado a la respuesta.</param>
    /// <returns>400</returns>
    [NonAction]
    protected ActionResult CustomBadRequest(string? message = null)
    {
        string traceID = HttpContext.TraceIdentifier;
        int statusCode = (int)HttpStatusCode.BadRequest;
        var result = new BaseErrorResponse(traceID, statusCode, message)
        {
            Instance = HttpContext.Request.Path
        };

        return BadRequest(result);
    }

    /// <summary>
    /// Representa una respuesta Error.
    /// </summary>
    /// <param name="message">Mensaje asociado a la respuesta.</param>
    /// <returns>500</returns>
    [NonAction]
    protected ActionResult CustomError(string? message = null)
    {
        string traceID = HttpContext.TraceIdentifier;
        int statusCode = (int)HttpStatusCode.InternalServerError;
        var result = new BaseErrorResponse(traceID, statusCode, message)
        {
            Instance = HttpContext.Request.Path
        };

        return new InternalServerErrorObjectResult(result);
    }
}
