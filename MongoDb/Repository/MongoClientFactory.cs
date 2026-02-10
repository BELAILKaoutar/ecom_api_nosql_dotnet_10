using ecom_api_nosql_.MongoDb.Interface;
using ecom_api_nosql_.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ecom_api_nosql_.MongoDb.Repository;

public class MongoClientFactory : IMongoClientFactory
{
    protected readonly IMongoDatabase _database;

    protected readonly MongoClient _client;

    protected readonly IOptions<MongoDbConfiguration> _options;

    public MongoClientFactory(IOptions<MongoDbConfiguration> options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));

        var connectionString = _options.Value.ConnectionString 
            ?? throw new InvalidOperationException("MongoDB ConnectionString is not configured");
        
        var database = _options.Value.Database 
            ?? throw new InvalidOperationException("MongoDB Database is not configured");

        _client = new MongoClient(connectionString);
        _database = _client.GetDatabase(database);

    }

    public IMongoCollection<TDocument> GetMongoCollection<TDocument>(string collectionName) where TDocument : class, new()
    {
        return _database.GetCollection<TDocument>(collectionName);
    }
}