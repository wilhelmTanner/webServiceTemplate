namespace Tanner.Template.Base.DataAccess.SQL;

public sealed partial class TemplateSQLRepository : ITemplateSQLRepository
{
    private readonly AppSettings _appSettings;

    public TemplateSQLRepository(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings?.Value;
        Init();            
    }

    private void Init()
    {
        if (string.IsNullOrEmpty(_appSettings.ConnectionStringKey))
            throw new Exception($"No se encontró la cadena de conexión");
    }

    public string GetConnectionString()
    {
        return _appSettings.ConnectionStringKey;
    }
}