namespace Tanner.Template.Base.Common.Configurations;

public class SwaggerSettings
{
    public static string SectionName = nameof(SwaggerSettings);

    public string Title { get; set; }

    public string Description { get; set; }

    public string ContactName { get; set; }

    public string ContactUrl { get; set; }

    public string ContactEmail { get; set; }


    public SwaggerVersionConfiguration[] Versions { get; set; }

    public SwaggerVersionConfiguration GetDefaultVersion()
    {
        SwaggerVersionConfiguration result = Versions.First(t => t.IsDefault);
        return result;
    }
}

public class SwaggerVersionConfiguration
{
    public string Version { get; set; }

    public bool IsDefault { get; set; }

    public string EndpointUrl { get; set; }

    public string EndpointDescription { get; set; }

    public OpenApiInfo GetInfo(string contactEmail, string contactName, string contactUrl, string title, string description)
    {
        var result = new OpenApiInfo
        {
            Contact = new OpenApiContact
            {
                Email = contactEmail,
                Name = contactName,
                Url = new Uri(contactUrl)
            },
            Description = description,
            Title = title,
            Version = Version
        };
        return result;
    }
}
