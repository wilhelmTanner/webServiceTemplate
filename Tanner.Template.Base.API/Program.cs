var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;

    config.AddJsonFile("settings/appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"settings/appsettings.{env.EnvironmentName}.json",
            optional: true, reloadOnChange: true);

    config.AddEnvironmentVariables();

    config.AddCommandLine(args);
}).ConfigureLogging((context, logging) => {
    logging.AddFilter("System.Net.Http.HttpClient", LogLevel.Error);
    logging.AddFilter("Microsoft.EntityFrameworkCore.Model.Validation", LogLevel.Error);
});

#region Add services to the container.

builder.Services.AddConfigurations(builder.Configuration);
builder.Services.AddAppInsights();
builder.Services.AddMongoServices();
builder.Services.AddCustomHttpClient();
builder.Services.AddCustomServices();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddHealthChecks();

builder.Services
    .AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddModelValidator();
builder.Services.AddCorsPolicy();

#endregion

WebApplication app = builder.Build();

#region  Configure pipeline

var logger = app.Services.GetRequiredService<ILoggerFactory>();
app.ConfigureExceptionHandler(logger);

string basePath = builder.Configuration.GetValue<string>("BasePath");
if (!string.IsNullOrEmpty(basePath))
{
    app.UsePathBase($"/{basePath}/");
}

app.UseCustomSwagger(basePath);

app.UseHttpsRedirection();
app.UseCors("DefaultPolicy");
app.UseRouting();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("TraceIdentifier", context.TraceIdentifier);
    await next.Invoke();
});

app.UseEndpoints((endpoints) =>
{
    endpoints.MapHealthChecks("/health");
    endpoints.MapControllers();
});

#endregion

app.Run();