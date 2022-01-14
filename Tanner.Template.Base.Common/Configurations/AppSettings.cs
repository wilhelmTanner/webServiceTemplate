namespace Tanner.Template.Base.Common.Configurations;

public class AppSettings : IAppSettings
{
    public string ConnectionStringKey { get; set; }
}


public interface IAppSettings
{
    string ConnectionStringKey { get; set; }
}
