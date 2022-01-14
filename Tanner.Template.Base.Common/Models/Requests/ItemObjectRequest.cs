namespace Tanner.Template.Base.Common.Models.Requests;

public class ItemObjectRequest
{
    [JsonPropertyName("Nombre")]
    public string Name { get; set; }

    [JsonPropertyName("Anno")]
    public int Year { get; set; }

    [JsonPropertyName("Activo")]
    public bool Active { get; set; }
}