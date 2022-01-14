namespace Tanner.Template.Base.API.Filters;

[ExcludeFromCodeCoverage]
public class AuthorizationHeaderOperationFilter : IOperationFilter
{
    private readonly string _description;

    private readonly bool _required;

    private readonly string _defaultValue;

    public AuthorizationHeaderOperationFilter(string description, string defaultValue, bool required = false)
    {
        _description = description;
        _required = required;
        _defaultValue = defaultValue;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        IList<Microsoft.AspNetCore.Mvc.Filters.FilterDescriptor> filterDescriptors = context.ApiDescription.ActionDescriptor.FilterDescriptors;
        bool isAuthorized = filterDescriptors.Select(filterInfo => filterInfo.Filter).Any(filter => filter is AuthorizeFilter);
        bool allowAnonymous = filterDescriptors.Select(filterInfo => filterInfo.Filter).Any(filter => filter is IAllowAnonymousFilter);

        if (isAuthorized && !allowAnonymous)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Description = _description,
                Required = _required,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString(_defaultValue)
                }
            });
        }
    }
}
