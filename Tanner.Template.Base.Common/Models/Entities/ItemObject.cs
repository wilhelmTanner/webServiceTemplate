namespace Tanner.Template.Base.Common.Models.Entities;

public class ItemObject
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Name { get; set; }

    public int Year { get; set; }

    public bool Active { get; set; }
}