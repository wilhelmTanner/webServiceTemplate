namespace Tanner.Template.Base.DataAccess.Mongo;

public static class TemplateMongoRepositoryExtensions
{
    public static IMongoQueryable<ItemObject> FilterItemObjects(this IMongoQueryable<ItemObject> items,
        uint minYear, uint maxYear) => items.Where(e => e.Year >= minYear && e.Year <= maxYear);

    public static IMongoQueryable<ItemObject> Search(this IMongoQueryable<ItemObject> items, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return items;

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return items.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
    }

    public static IMongoQueryable<ItemObject> Sort(this IMongoQueryable<ItemObject> items, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return items.OrderBy(e => e.Name);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<ItemObject>(orderByQueryString, OrderTypeEnum.Linq);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return items.OrderBy(e => e.Name);

        var result = items.OrderBy(orderQuery);

        return (IMongoQueryable<ItemObject>)result;
    }
}
