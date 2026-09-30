using MongoDB.Bson;
using MongoDB.Driver;

namespace Mellon.Infrastructure.Storage.MongoDB;

public static partial class MellonMongoOperations
{
    public static async Task<TDao?> FindDocumentById<TDao>(IMongoDatabase database, string collectionName, string id)
        where TDao : MellonMongoDocument
    {
        if (!ObjectId.TryParse(id, out var objectId))
            return null;

        var filter = Builders<TDao>.Filter.Eq("_id", objectId);
        return await database.GetCollection<TDao>(collectionName).Find(filter).FirstOrDefaultAsync();
    }

    public static async Task<TDao?> FindOneByFilter<TDao>(
        IMongoDatabase database,
        string collectionName,
        FilterDefinition<TDao> filter)
        where TDao : MellonMongoDocument
    {
        return await database.GetCollection<TDao>(collectionName).Find(filter).FirstOrDefaultAsync();
    }

    public static async Task<List<TDao>> FindAllDocuments<TDao>(IMongoDatabase database, string collectionName)
        where TDao : MellonMongoDocument
    {
        return await database.GetCollection<TDao>(collectionName).Find(FilterDefinition<TDao>.Empty).ToListAsync();
    }

    public static async Task<List<TDao>> FindDocumentsByFilter<TDao>(
        IMongoDatabase database,
        string collectionName,
        FilterDefinition<TDao> filter,
        SortDefinition<TDao>? sort = null)
        where TDao : MellonMongoDocument
    {
        var find = database.GetCollection<TDao>(collectionName).Find(filter);
        if (sort is not null)
            find = find.Sort(sort);

        return await find.ToListAsync();
    }
}
