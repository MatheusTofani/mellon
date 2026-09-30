using Mellon.Infrastructure.Storage.MongoDB;
using Mellon.Application.Interfaces.Repository;
using Mellon.Domain.Entities;

namespace Mellon.Infrastructure.Repositories.SalonRepository;

public class SalonRepositoryCommands(IMongoConnectionFactory factory)
    : IRepositoryCommand<Task<Salon>, Salon>
{
    public async Task<Salon> Handle(Salon input)
    {
        var db = factory.GetDatabase();
        var document = SalonMongoDocument.FromDomain(input);

        await MellonMongoOperations.InsertDocument(db, document);
        return document.ToDomain();
    }
}
