namespace Tanner.Template.Base.API.ActionResults;

/// <summary>
/// 
/// </summary>
public class InternalServerErrorObjectResult : ObjectResult
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="error"></param>
    public InternalServerErrorObjectResult(object error)
        : base(error)
    {
        StatusCode = StatusCodes.Status500InternalServerError;
    }
}
