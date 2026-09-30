using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Mellon.Domain.Errors;

namespace Mellon.Infrastructure.Storage.MongoDB;

public abstract class MellonMongoDocument
{
    [BsonIgnore]
    public abstract string CollectionName { get; }

    public DateTime CreatedAt { get; set; }

    public abstract Task WriteIndexes(IMongoDatabase database);

    protected static ObjectId ParseObjectId(string id)
    {
        if (!ObjectId.TryParse(id, out var objectId))
            throw new UnprocessableEntityException("Id inválido.");
        return objectId;
    }
}
