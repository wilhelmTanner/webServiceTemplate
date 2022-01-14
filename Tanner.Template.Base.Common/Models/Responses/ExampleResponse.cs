namespace Tanner.Template.Base.Common.Models.Responses;

public class ExampleResponse
{
    [JsonPropertyName("Id")]
    public int Id { get; set; }

    [JsonPropertyName("Descripcion")]
    public string Description { get; set; }

    [JsonPropertyName("Valor")]
    public int Value { get; set; }

    public ExampleResponse(ExampleObject exampleObject)
    {
        Id = exampleObject.Id;
        Description = exampleObject.Descripcion;
        Value = exampleObject.Valor;
    }
}