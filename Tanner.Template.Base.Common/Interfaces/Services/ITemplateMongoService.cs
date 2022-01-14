﻿namespace Tanner.Template.Base.Common.Interfaces.Services;

public interface ITemplateMongoService
{
    /// <summary>
    /// Obtener item por Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ItemObject> GetItemByIdAsync(string id);

    /// <summary>
    /// Obtener todos los items.
    /// </summary>
    /// <param name="itemObjectParams"></param>
    /// <returns></returns>
    Task<(IEnumerable<ItemObject>, int)> GetAllAsync(ItemObjectParameters itemObjectParams);

    /// <summary>
    /// Insertar item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    Task<ItemObject> InsertItemAsync(ItemObject item);

    /// <summary>
    /// Actualizar item.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    Task UpdateItemAsync(ItemObject item);
}