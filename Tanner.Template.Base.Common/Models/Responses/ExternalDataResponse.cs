namespace Tanner.Template.Base.Common.Models.Responses;

public class ExternalDataResponse
{
    [JsonPropertyName("IdUsuario")]
    public int UserId { get; set; }

    [JsonPropertyName("Titulo")]
    public string Title { get; set; }

    [JsonPropertyName("CuerpoMensaje")]
    public string Body { get; set; }

    public ExternalDataResponse(ExternalServiceResponse response)
    {
        UserId = response.UserId;
        Title = response.Title;
        Body = response.Body;
    }
}