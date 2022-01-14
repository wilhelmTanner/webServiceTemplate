namespace Tanner.Template.Base.API.Filters;

[ExcludeFromCodeCoverage]
public class VersionFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        string infoVersion = swaggerDoc.Info.Version;

        List<ApiDescription> apis = context.ApiDescriptions.ToList();
        List<OperationType> operationTypes = Enum.GetValues(typeof(OperationType)).OfType<OperationType>().ToList();

        foreach (KeyValuePair<string, OpenApiPathItem> item in swaggerDoc.Paths)
        {
            var value = item.Value;
            foreach (OperationType type in operationTypes)
            {
                if (value.Operations.ContainsKey(type))
                {
                    ApiDescription api = apis.First(t => $"/{t.RelativePath}" == item.Key && t.HttpMethod == type.ToString().ToUpper());
                    bool existVersion = ExistVersion(api, infoVersion);
                    if (!existVersion)
                    {
                        value.Operations.Remove(type);
                    }
                    else
                    {
                        if (value.Operations[type].Parameters == null)
                        {
                            value.Operations[type].Parameters = new List<OpenApiParameter>();
                        }
                        value.Operations[type].Parameters.Add(new OpenApiParameter
                        {
                            Name = "Api-Version",
                            In = ParameterLocation.Header,
                            Description = "API version",
                            Required = false,
                            Schema = new OpenApiSchema
                            {
                                Type = "string",
                                Default = new OpenApiString(infoVersion)
                            },
                        });
                    }
                }
            }
        }
    }

    private bool ExistVersion(ApiDescription api, string infoVersion)
    {
        IList<object> endpointMetadata = api.ActionDescriptor.EndpointMetadata;

        foreach (object item in endpointMetadata)
        {
            if (item is Microsoft.AspNetCore.Mvc.ApiVersionAttribute apiVersion)
            {
                string actionVersion = apiVersion.Versions.Select(t => t.ToString()).FirstOrDefault();
                if (actionVersion == infoVersion)
                {
                    return true;
                }
            }
        }
        return false;
    }
}


