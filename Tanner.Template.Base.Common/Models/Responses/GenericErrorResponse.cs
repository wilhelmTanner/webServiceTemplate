namespace Tanner.Template.Base.Common.Models.Responses;

/// <summary>
/// Respuesta genérica de errores
/// </summary>
public class GenericErrorResponse
{
    [JsonPropertyName("Errores")]
    public List<GenericErrorItem> Errors { get; set; }
}

public class GenericErrorItem
{
    [JsonPropertyName("IdError")]
    public string ErrorId { get; set; }

    [JsonPropertyName("Mensaje")]
    public string Message { get; set; }
}