namespace Tanner.Template.Base.API.Extensions;

/// <summary>
/// Clase para la inicialización de la aplicación
/// </summary>
[ExcludeFromCodeCoverage]
public static class ServicesCollectionExtensions
{
    /// <summary>
    /// Permite configurar el Application Insights
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddAppInsights(
       this IServiceCollection services)
    {
        services.AddApplicationInsightsTelemetry();
        services.AddApplicationInsightsKubernetesEnricher();
        services.AddApplicationInsightsTelemetryProcessor<HealthRequestFilter>();
        services.Configure<TelemetryConfiguration>(
            (o) =>
            {
                o.TelemetryInitializers.Add(new AddRoleNameInitializer());
                o.TelemetryInitializers.Add(new OperationCorrelationTelemetryInitializer());
            });

        return services;
    }

    /// <summary>
    /// Permite configurar lo necesario para la configuración de la aplicación
    /// y los servicios
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static IServiceCollection AddConfigurations(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
        services.Configure<ServiceSettings>(configuration.GetSection(nameof(ServiceSettings)));
        services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
        services.Configure<SwaggerSettings>(configuration.GetSection(nameof(SwaggerSettings)));

        return services;
    }

    /// <summary>
    /// Permite la injección de los servicios
    /// en el contenedor de dependecias
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddCustomServices(
        this IServiceCollection services)
    {
        services.AddScoped<IFakeEndpointClient, FakeEndpointClient>();
        services.AddScoped<ITemplateBaseService, TemplateBaseService>();
        services.AddScoped<ITemplateMongoService, TemplateMongoService>();
        services.AddScoped<ITemplateSQLRepository, TemplateSQLRepository>();
        services.AddScoped<ITemplateMongoRepository, TemplateMongoRepository>();

        return services;
    }

    /// <summary>
    /// Permite configurar Swagger para la documentación del api
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        IServiceProvider provider = services.BuildServiceProvider();
        SwaggerSettings swaggerConf = GetSwaggerSettings(provider);

        SwaggerVersionConfiguration versionDefault = swaggerConf.GetDefaultVersion();

        services.AddApiVersioning(o =>
        {
            o.ReportApiVersions = true;
            o.ApiVersionReader = new HeaderApiVersionReader("api-version", "Api-Version", "Version", "version");
            o.AssumeDefaultVersionWhenUnspecified = true;
            if (!string.IsNullOrEmpty(versionDefault.Version))
            {
                o.DefaultApiVersion = ApiVersion.Parse(versionDefault.Version);
            }
        });

        services.AddSwaggerGen(swagger =>
        {
            foreach (SwaggerVersionConfiguration version in swaggerConf.Versions)
            {
                var info = version.GetInfo(swaggerConf.ContactEmail, swaggerConf.ContactName, swaggerConf.ContactUrl, swaggerConf.Title, swaggerConf.Description);
                swagger.SwaggerDoc(version.Version, info);
            }

            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            swagger.IncludeXmlComments(xmlPath);

            if (swaggerConf.Versions != null && swaggerConf.Versions.Count() > 1)
                swagger.DocumentFilter<VersionFilter>();

            swagger.SchemaFilter<SwaggerIgnoreFilter>();
        });
        return services;
    }

    /// <summary>
    /// Permite la configuración de clientes Https
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddCustomHttpClient(this IServiceCollection services)
    {
        var sp = services.BuildServiceProvider();
        var serviceSettings = sp.GetRequiredService<IOptions<ServiceSettings>>().Value;

        services.AddHttpClient(nameof(FakeEndpointClient), c =>
        {
            c.BaseAddress = new Uri(serviceSettings.Host);
            //Agregar versión, si es que aplica
            //c.DefaultRequestHeaders.Add("api-version", "1.0");
        }).SetHandlerLifetime(TimeSpan.FromMinutes(5));

        return services;
    }

    /// <summary>
    /// Permite configurar el servicio de MongoDb
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddMongoServices(
       this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient, MongoClient>(sp =>
        {
            var mongoConfig = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(mongoConfig.Uri);
        });

        services.AddSingleton<TemplateMongoRepository>();

        return services;
    }

    /// <summary>
    /// Permite configurar el comportamiento de Mvc
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddModelValidator(this IServiceCollection services)
    {
        services.AddMvc()
                     .AddJsonOptions(options =>
                             options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                        );
        
        return services;
    }

    /// <summary>
    /// Permite configurar las políticas de CORS
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("DefaultPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
        });


        return services;
    }

    /// <summary>
    /// Obtiene settings para Swagger
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    private static SwaggerSettings GetSwaggerSettings(IServiceProvider provider)
    {
        var swaggerSettings = provider.GetRequiredService<IOptions<SwaggerSettings>>();
        SwaggerSettings result = swaggerSettings.Value;
        return result;
    }

    /// <summary>
    /// Configuración de Swagger
    /// </summary>
    /// <param name="app"></param>
    /// <param name="basePath"></param>
    public static void UseCustomSwagger(this IApplicationBuilder app, string basePath)
    {
        IServiceProvider provider = app.ApplicationServices;
        SwaggerSettings swaggerConf = GetSwaggerSettings(provider);

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            foreach (SwaggerVersionConfiguration version in swaggerConf.Versions)
            {
                c.SwaggerEndpoint($"/{basePath}{version.EndpointUrl}", version.EndpointDescription);
            }
        });
    }
}
