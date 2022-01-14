namespace Tanner.Template.Base.Common.Configurations;

public class MongoDbSettings : IMongoDbSettings
{
    public string Uri { get; set; }
    public string Database { get; set; }
    public string CollectionName { get; set; }
}

public interface IMongoDbSettings
{
    string Uri { get; set; }
    string Database { get; set; }
    string CollectionName { get; set; }
}
