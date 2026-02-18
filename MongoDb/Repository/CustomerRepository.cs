using ecom_api_nosql_.Common.Pagination;
using ecom_api_nosql_.Models;
using ecom_api_nosql_.MongoDb.Interface;
using ecom_api_nosql_.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ecom_api_nosql_.MongoDb.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly IMongoCollection<Customer> _customersCollection;

    public CustomerRepository(IMongoClientFactory mongoClientFactory, IOptions<MongoDbConfiguration> options)
    {
        var collectionName = options.Value.Collections?["Customers"] ?? "Customers";
        _customersCollection = mongoClientFactory.GetMongoCollection<Customer>(collectionName);
    }

    public async Task<List<Customer>> GetAllAsync()
        => await _customersCollection.Find(_ => true).ToListAsync();

    public async Task<Customer?> GetByIdAsync(string id)
        => await _customersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<Customer> CreateAsync(Customer customer)
    {
        customer.DateCreation = DateTime.UtcNow;
        await _customersCollection.InsertOneAsync(customer);
        return customer;
    }

    public async Task<Customer?> UpdateAsync(string id, Customer customer)
    {
        var result = await _customersCollection.ReplaceOneAsync(x => x.Id == id, customer);
        if (result.ModifiedCount == 0) return null;
        return customer;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _customersCollection.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }

    // ✅ Pagination
    public async Task<PagedResult<Customer>> GetPagedAsync(PagedQuery query, CancellationToken ct = default)
    {
        query.Normalize();

        var filter = Builders<Customer>.Filter.Empty;

        var total = await _customersCollection.CountDocumentsAsync(filter, cancellationToken: ct);

        var items = await _customersCollection.Find(filter)
            .SortByDescending(x => x.DateCreation)
            .Skip(query.Skip)
            .Limit(query.PageSize)
            .ToListAsync(ct);

        return new PagedResult<Customer>
        {
            Items = items,
            TotalCount = total,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }
}
