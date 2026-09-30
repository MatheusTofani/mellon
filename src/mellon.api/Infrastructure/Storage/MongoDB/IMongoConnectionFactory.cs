using MongoDB.Driver;

namespace Mellon.Infrastructure.Storage.MongoDB;

public interface IMongoConnectionFactory
{
    IMongoDatabase GetDatabase();
}
