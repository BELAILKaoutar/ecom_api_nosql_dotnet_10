using ecom_api_nosql_.Models;
using ecom_api_nosql_.MongoDb.Interface;
using ecom_api_nosql_.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ecom_api_nosql_.MongoDb.Repository;

/// <summary>
/// Repository implementation for Product operations
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _productsCollection;

    public ProductRepository(IMongoClientFactory mongoClientFactory, IOptions<MongoDbConfiguration> options)
    {
        var collectionName = options.Value.Collections?["Products"] ?? "Products";
        _productsCollection = mongoClientFactory.GetMongoCollection<Product>(collectionName);
    }

    /// <inheritdoc />
    public async Task<List<Product>> GetAllAsync()
    {
        return await _productsCollection.Find(_ => true).ToListAsync()
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Product?> GetByIdAsync(string id)
    {
        return await _productsCollection.Find(p => p.Id == id).FirstOrDefaultAsync()
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<List<Product>> GetByCategorieAsync(string categorie)
    {
        return await _productsCollection.Find(p => p.Categorie == categorie).ToListAsync()
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<Product> CreateAsync(Product product)
    {
        product.DateAjout = DateTime.UtcNow;
        await _productsCollection.InsertOneAsync(product)
            .ConfigureAwait(false);
        return product;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(string id, Product product)
    {
        var result = await _productsCollection.ReplaceOneAsync(p => p.Id == id, product)
            .ConfigureAwait(false);
        return result.ModifiedCount > 0;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _productsCollection.DeleteOneAsync(p => p.Id == id)
            .ConfigureAwait(false);
        return result.DeletedCount > 0;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsAsync(string id)
    {
        var count = await _productsCollection.CountDocumentsAsync(p => p.Id == id)
            .ConfigureAwait(false);
        return count > 0;
    }
}
