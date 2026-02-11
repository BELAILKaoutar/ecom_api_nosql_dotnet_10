using ecom_api_nosql_.Models;
using ecom_api_nosql_.MongoDb.Interface;
using ecom_api_nosql_.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ecom_api_nosql_.MongoDb.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly IMongoCollection<Order> _ordersCollection;

    public OrderRepository(IMongoClientFactory mongoClientFactory, IOptions<MongoDbConfiguration> options)
    {
        var collectionName = options.Value.Collections?["Orders"] ?? "Orders";
        _ordersCollection = mongoClientFactory.GetMongoCollection<Order>(collectionName);
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _ordersCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(string id)
    {
        return await _ordersCollection.Find(o => o.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Order>> GetByCustomerIdAsync(string customerId)
    {
        return await _ordersCollection.Find(o => o.CustomerId == customerId).ToListAsync();
    }

    public async Task<Order> CreateAsync(Order order)
    {
        await _ordersCollection.InsertOneAsync(order);
        return order;
    }

    public async Task<bool> UpdateAsync(string id, Order order)
    {
        var result = await _ordersCollection.ReplaceOneAsync(o => o.Id == id, order);
        return result.ModifiedCount > 0;
    }


    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _ordersCollection.DeleteOneAsync(o => o.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        var count = await _ordersCollection.CountDocumentsAsync(o => o.Id == id);
        return count > 0;
    }
}
