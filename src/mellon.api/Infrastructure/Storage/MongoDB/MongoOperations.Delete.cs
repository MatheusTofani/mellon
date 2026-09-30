using MongoDB.Bson;
using MongoDB.Driver;
using Mellon.Domain.Errors;

namespace Mellon.Infrastructure.Storage.MongoDB;

public static partial class MellonMongoOperations
{
    public static async Task DeleteDocumentById<TDao>(IMongoDatabase database, string collectionName, string id)
        where TDao : MellonMongoDocument
    {
        if (!ObjectId.TryParse(id, out var objectId))
            throw new NotFoundException($"Documento não encontrado: {id}");

        var filter = Builders<TDao>.Filter.Eq("_id", objectId);
        var result = await database.GetCollection<TDao>(collectionName).DeleteOneAsync(filter);

        if (result.DeletedCount == 0)
            throw new NotFoundException($"Documento não encontrado: {id}");
    }
}
