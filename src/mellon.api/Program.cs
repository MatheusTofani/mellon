using Mellon.Application.Interfaces.Repository;
using Mellon.Application.UseCase;
using Mellon.Application.UseCase.SalonUseCases.Creates;
using Mellon.Domain.Entities;
using Mellon.Infrastructure.Repositories.SalonRepository;
using Mellon.Infrastructure.Storage.MongoDB;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMongoClient>(_ =>
{
    var connectionString = builder.Configuration["MongoDb:ConnectionString"]
        ?? throw new InvalidOperationException("Configure MongoDb:ConnectionString para conectar ao MongoDB.");
    return new MongoClient(connectionString);
});
builder.Services.AddSingleton<IMongoDatabase>(services =>
{
    var databaseName = builder.Configuration["MongoDb:DatabaseName"]
        ?? new MongoUrl(builder.Configuration["MongoDb:ConnectionString"]).DatabaseName;
    if (string.IsNullOrWhiteSpace(databaseName))
        throw new InvalidOperationException("Configure MongoDb:DatabaseName ou informe o banco na connection string.");

    return services.GetRequiredService<IMongoClient>().GetDatabase(databaseName);
});
builder.Services.AddSingleton<IMongoConnectionFactory, MongoConnectionFactory>();
builder.Services.AddScoped<IRepositoryCommand<Task<Salon>, Salon>, SalonRepositoryCommands>();
builder.Services.AddScoped<IUseCase<Task<Salon>, CreateSalonInput>, CreateSalonUseCase>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Mellon API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
