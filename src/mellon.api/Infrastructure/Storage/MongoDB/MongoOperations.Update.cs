using MongoDB.Driver;
using Mellon.Domain.Errors;

namespace Mellon.Infrastructure.Storage.MongoDB;

public static partial class MellonMongoOperations
{
    public static async Task<TDao> FindOneAndUpdate<TDao>(
        IMongoDatabase database,
        string collectionName,
        FilterDefinition<TDao> filter,
        UpdateDefinition<TDao> update)
        where TDao : MellonMongoDocument
    {
        var options = new FindOneAndUpdateOptions<TDao> { ReturnDocument = ReturnDocument.After };
        var result  = await database.GetCollection<TDao>(collectionName).FindOneAndUpdateAsync(filter, update, options);

        if (result is null)
            throw new NotFoundException("Registro não encontrado.");

        return result;
    }

    public static async Task<TDao> ReplaceDocument<TDao>(
        IMongoDatabase database,
        string collectionName,
        FilterDefinition<TDao> filter,
        TDao document)
        where TDao : MellonMongoDocument
    {
        var result = await database.GetCollection<TDao>(collectionName).ReplaceOneAsync(filter, document);

        if (result.MatchedCount == 0)
            throw new NotFoundException("Registro não encontrado.");

        return document;
    }

    public static async Task UpdateOne<TDao>(
        IMongoDatabase database,
        string collectionName,
        FilterDefinition<TDao> filter,
        UpdateDefinition<TDao> update,
        IEnumerable<ArrayFilterDefinition>? arrayFilters = null,
        bool requireMatch = true)
        where TDao : MellonMongoDocument
    {
        var options = arrayFilters is null ? null : new UpdateOptions { ArrayFilters = arrayFilters.ToList() };
        var result  = await database.GetCollection<TDao>(collectionName).UpdateOneAsync(filter, update, options);

        if (requireMatch && result.MatchedCount == 0)
            throw new NotFoundException("Registro não encontrado.");
    }
}
