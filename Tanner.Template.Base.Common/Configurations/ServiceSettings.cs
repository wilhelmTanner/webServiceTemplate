namespace Tanner.Template.Base.Common.Configurations;

public class ServiceSettings : IServiceSettings
{
    public string PostUri { get; set; }
    public string Host { get; set; }
}

public interface IServiceSettings
{
    string PostUri { get; set; }
    string Host { get; set; }
}
