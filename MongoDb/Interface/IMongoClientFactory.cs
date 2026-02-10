using MongoDB.Driver;

namespace ecom_api_nosql_.MongoDb.Interface;

public interface IMongoClientFactory
{
    IMongoCollection<TDocument> GetMongoCollection<TDocument>(string collectionName) where TDocument : class, new();

}