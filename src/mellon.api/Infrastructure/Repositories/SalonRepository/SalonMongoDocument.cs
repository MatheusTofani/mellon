using Mellon.Infrastructure.Storage.MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Mellon.Infrastructure.Repositories.SalonRepository;

[BsonIgnoreExtraElements]
public class SalonMongoDocument : MellonMongoDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public override string CollectionName => CollectionNameStatic;
    public static string CollectionNameStatic => "Salon";

    public override async Task WriteIndexes(IMongoDatabase database)
    {
        var collection = database.GetCollection<SalonMongoDocument>(CollectionNameStatic);
        await collection.Indexes.CreateOneAsync(new CreateIndexModel<SalonMongoDocument>(
            Builders<SalonMongoDocument>.IndexKeys.Ascending(x => x.Name),
            new CreateIndexOptions { Name = "Name_Index" }));
    }
}