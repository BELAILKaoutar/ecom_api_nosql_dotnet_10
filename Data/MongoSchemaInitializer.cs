using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ecom_api_nosql_.Settings;

namespace ecom_api_nosql_.Data;

public class MongoSchemaInitializer
{
    private readonly IMongoDatabase _db;

    public MongoSchemaInitializer(IOptions<MongoDbConfiguration> options)
    {
        var cfg = options.Value;
        var client = new MongoClient(cfg.ConnectionString);
        _db = client.GetDatabase(cfg.Database);
    }

    public async Task EnsureAsync()
    {
        await EnsureCollectionWithSchema("Products", SchemaObjectOnly());
        await EnsureCollectionWithSchema("Customers", SchemaObjectOnly());
        await EnsureCollectionWithSchema("Orders", SchemaObjectOnly());

        await EnsureIndexesAsync();
    }

    private async Task EnsureCollectionWithSchema(string name, BsonDocument schema)
    {
        try
        {
            var cmd = new BsonDocument
            {
                { "collMod", name },
                { "validator", schema },
                { "validationLevel", "moderate" }
            };
            await _db.RunCommandAsync<BsonDocument>(cmd);
        }
        catch (MongoCommandException ex) when (ex.CodeName == "NamespaceNotFound")
        {
            var create = new BsonDocument
            {
                { "create", name },
                { "validator", schema },
                { "validationLevel", "moderate" }
            };
            await _db.RunCommandAsync<BsonDocument>(create);
        }
    }

    private async Task EnsureIndexesAsync()
    {
        var customers = _db.GetCollection<BsonDocument>("Customers");

        await customers.Indexes.CreateOneAsync(
            new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("email"),
                new CreateIndexOptions { Unique = true, Name = "ux_customers_email" }
            )
        );
    }

    private static BsonDocument SchemaObjectOnly()
        => new BsonDocument("$jsonSchema", new BsonDocument { { "bsonType", "object" } });
}
