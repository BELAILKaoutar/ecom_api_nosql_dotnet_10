using ecom_api_nosql_.Common.Pagination;
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
        => await _ordersCollection.Find(_ => true).ToListAsync();

    public async Task<Order?> GetByIdAsync(string id)
        => await _ordersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<Order> CreateAsync(Order order)
    {
        order.DateCommande = DateTime.UtcNow;
        await _ordersCollection.InsertOneAsync(order);
        return order;
    }

    public async Task<Order?> UpdateStatutAsync(string id, string statut)
    {
        var update = Builders<Order>.Update.Set(x => x.Statut, statut);
        var result = await _ordersCollection.UpdateOneAsync(x => x.Id == id, update);
        if (result.ModifiedCount == 0) return null;
        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _ordersCollection.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }

    // ✅ Pagination
    public async Task<PagedResult<Order>> GetPagedAsync(PagedQuery query, CancellationToken ct = default)
    {
        query.Normalize();

        var filter = Builders<Order>.Filter.Empty;

        var total = await _ordersCollection.CountDocumentsAsync(filter, cancellationToken: ct);

        var items = await _ordersCollection.Find(filter)
            .SortByDescending(x => x.DateCommande)
            .Skip(query.Skip)
            .Limit(query.PageSize)
            .ToListAsync(ct);

        return new PagedResult<Order>
        {
            Items = items,
            TotalCount = total,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }
}
