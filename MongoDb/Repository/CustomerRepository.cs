using ecom_api_nosql_.Models;
using ecom_api_nosql_.MongoDb.Interface;
using ecom_api_nosql_.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ecom_api_nosql_.MongoDb.Repository;

/// <summary>
/// Repository implementation for Customer operations
/// </summary>
public class CustomerRepository : ICustomerRepository
{
    private readonly IMongoCollection<Customer> _customersCollection;

    public CustomerRepository(
        IMongoClientFactory mongoClientFactory,
        IOptions<MongoDbConfiguration> options)
    {
        var collectionName = options.Value.Collections?["Customers"] ?? "Customers";
        _customersCollection = mongoClientFactory.GetMongoCollection<Customer>(collectionName);
    }

    /// <inheritdoc />
    public async Task<List<Customer>> GetAllAsync()
    {
        return await _customersCollection
            .Find(_ => true)
            .ToListAsync()
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Customer?> GetByIdAsync(string id)
    {
        return await _customersCollection
            .Find(c => c.Id == id)
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Customer> CreateAsync(Customer customer)
    {
        await _customersCollection
            .InsertOneAsync(customer)
            .ConfigureAwait(false);

        return customer;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(string id, Customer customer)
    {
        var result = await _customersCollection
            .ReplaceOneAsync(c => c.Id == id, customer)
            .ConfigureAwait(false);

        return result.ModifiedCount > 0;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _customersCollection
            .DeleteOneAsync(c => c.Id == id)
            .ConfigureAwait(false);

        return result.DeletedCount > 0;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsAsync(string id)
    {
        var count = await _customersCollection
            .CountDocumentsAsync(c => c.Id == id)
            .ConfigureAwait(false);

        return count > 0;
    }
}
