using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Persistence.Documents;

public class Base
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedOn { get; set; }
}
