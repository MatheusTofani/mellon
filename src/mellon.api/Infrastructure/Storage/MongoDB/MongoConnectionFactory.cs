using MongoDB.Driver;

namespace Mellon.Infrastructure.Storage.MongoDB;

public class MongoConnectionFactory(IMongoDatabase database) : IMongoConnectionFactory
{
    public IMongoDatabase GetDatabase() => database;
}
