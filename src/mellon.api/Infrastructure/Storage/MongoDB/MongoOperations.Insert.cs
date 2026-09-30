using MongoDB.Driver;
using Mellon.Domain.Errors;

namespace Mellon.Infrastructure.Storage.MongoDB;

public static partial class MellonMongoOperations
{
    public static async Task<TDao> InsertDocument<TDao>(IMongoDatabase database, TDao entity)
        where TDao : MellonMongoDocument
    {
        try
        {
            await database.GetCollection<TDao>(entity.CollectionName).InsertOneAsync(entity);
            return entity;
        }
        catch (MongoWriteException mwe) when (mwe.WriteError.Category == ServerErrorCategory.DuplicateKey)
        {
            throw new AlreadyExistsException("Registro já existe.", mwe);
        }
    }
}
