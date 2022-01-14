namespace Tanner.Template.Base.API.Filters;

[ExcludeFromCodeCoverage]
public class SwaggerIgnoreFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema?.Properties == null || !schema.Properties.Any() || context.Type == null)
            return;

        var excludedProperties = context.Type.GetProperties()
            .Where(t =>
                t.GetCustomAttribute<JsonIgnoreAttribute>()
                != null);

        foreach (var excludedProperty in excludedProperties)
        {
            if (schema.Properties.ContainsKey(ConvertCamelCase(excludedProperty.Name)))
                schema.Properties.Remove(excludedProperty.Name);
        }
    }

    private string ConvertCamelCase(string value)
    {
        if (value == null)
        {
            return null;
        }
        if (value.Length == 1)
        {
            return value.ToLower();
        }
        string result = $"{char.ToLowerInvariant(value[0])}{value.Substring(1)}";
        return result;
    }
}
