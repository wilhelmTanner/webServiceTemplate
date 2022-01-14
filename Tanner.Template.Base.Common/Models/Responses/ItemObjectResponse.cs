namespace Tanner.Template.Base.Common.Models.Responses;

public class ItemObjectResponse
{
    [JsonPropertyName("Id")]
    public string Id { get; set; }

    [JsonPropertyName("Nombre")]
    public string Name { get; set; }

    [JsonPropertyName("Anno")]
    public int Year { get; set; }

    [JsonPropertyName("Activo")]
    public bool Active { get; set; }

    public ItemObjectResponse(ItemObject item)
    {
        Id = item.Id;
        Name = item.Name;
        Year = item.Year;
        Active = item.Active;
    }
}