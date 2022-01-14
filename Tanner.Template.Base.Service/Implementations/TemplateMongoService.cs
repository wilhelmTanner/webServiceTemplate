namespace Tanner.Template.Base.Service.Implementations;

public class TemplateMongoService : ITemplateMongoService
{
    private readonly ITemplateMongoRepository _templateMongoRepository;

    public TemplateMongoService(ITemplateMongoRepository templateMongoRepository)
    {
        _templateMongoRepository = templateMongoRepository;
    }

    public async Task<(IEnumerable<ItemObject>, int)> GetAllAsync(ItemObjectParameters itemObjectParams)
    {
        (IEnumerable<ItemObject> items, int total) = await _templateMongoRepository.GetAllAsync(itemObjectParams);
        return (items, total);
    }

    /// <summary>
    /// Obtiene items
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ItemObject> GetItemByIdAsync(string id)
    {
        return await _templateMongoRepository.GetItemByIdAsync(id);
    }

    /// <summary>
    /// Inserta nuevo item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public async Task<ItemObject> InsertItemAsync(ItemObject item)
    {
        return await _templateMongoRepository.InsertItemAsync(item);
    }

    /// <summary>
    /// Actualiza item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public async Task UpdateItemAsync(ItemObject item)
    {
        await _templateMongoRepository.UpdateItemAsync(item);
    }
}