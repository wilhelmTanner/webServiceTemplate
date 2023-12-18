namespace Tanner.Template.Base.DataAccess.Mongo;

public class TemplateMongoRepository : ITemplateMongoRepository
{
    private readonly IMongoCollection<ItemObject> _exampleCollection;

    public TemplateMongoRepository(IMongoClient mongoClient,
        IOptions<MongoDbSettings> options)
    {
        var config = options.Value;
          
        //var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
        //ConventionRegistry.Register("CamelCase", camelCaseConvention, _ => true);

        _exampleCollection = mongoClient.GetDatabase(config.Database).GetCollection<ItemObject>(config.CollectionName);
    }

    public async Task<(IEnumerable<ItemObject>, int)> GetAllAsync(ItemObjectParameters itemObjectParams)
    {
        var itemObjects = _exampleCollection.AsQueryable()
            .FilterItemObjects(itemObjectParams.MinYear, itemObjectParams.MaxYear)
            .Search(itemObjectParams.SearchTerm);

        (IEnumerable<ItemObject> resources, int total) result = (new List<ItemObject>(), await itemObjects.CountAsync());

        itemObjects = itemObjects
            .Sort(itemObjectParams.Sort)
            .Skip(itemObjectParams.Offset)
            .Take(itemObjectParams.Limit);

        result.resources = await itemObjects.ToListAsync();
        return result;
    }

    public async Task<ItemObject> GetItemByIdAsync(string id)
    {
        FilterDefinition<ItemObject> filterQuery = Builders<ItemObject>
            .Filter
            .Where(c => c.Id == id);

        return await _exampleCollection.Find(filterQuery).FirstOrDefaultAsync();
    }

    public async Task<ItemObject> InsertItemAsync(ItemObject item)
    {
        await _exampleCollection.InsertOneAsync(item);
        return item;                                  
    }

    public async Task UpdateItemAsync(ItemObject item)
    {
        await _exampleCollection.ReplaceOneAsync(
            Builders<ItemObject>.Filter.Eq(c => c.Id, item.Id),
            item,
            new ReplaceOptions { IsUpsert = true });
    }
}