namespace Tanner.Template.Base.Common.Interfaces.Repositories;

public interface ITemplateMongoRepository
{
    /// <summary>
    /// Se actualiza item.
    /// </summary>
    /// <param name="cartola"></param>
    /// <returns></returns>
    Task UpdateItemAsync(ItemObject cartola);

    /// <summary>
    /// Se obtiene item por Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ItemObject> GetItemByIdAsync(string id);

    /// <summary>
    /// Se obtienen todos los items.
    /// </summary>
    /// <param name="itemObjectParams"></param>
    /// <returns></returns>
    Task<(IEnumerable<ItemObject>, int)> GetAllAsync(ItemObjectParameters itemObjectParams);

    /// <summary>
    /// Se inserta item.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    Task<ItemObject> InsertItemAsync(ItemObject item);
}