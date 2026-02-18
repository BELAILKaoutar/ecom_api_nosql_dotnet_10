using ecom_api_nosql_.Models;
using ecom_api_nosql_.MongoDb.Interface;
using ecom_api_nosql_.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ecom_api_nosql_.Common.Pagination;

namespace ecom_api_nosql_.MongoDb.Repository;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _productsCollection;

    public ProductRepository(IMongoClientFactory mongoClientFactory, IOptions<MongoDbConfiguration> options)
    {
        // ✅ Aligné avec MongoSchemaInitializer: "Products"
        var collectionName = options.Value.Collections?["Products"] ?? "Products";
        _productsCollection = mongoClientFactory.GetMongoCollection<Product>(collectionName);
    }

    public async Task<List<Product>> GetAllAsync()
        => await _productsCollection.Find(_ => true).ToListAsync().ConfigureAwait(false);

    public async Task<Product?> GetByIdAsync(string id)
        => await _productsCollection.Find(p => p.Id == id).FirstOrDefaultAsync().ConfigureAwait(false);

    public async Task<List<Product>> GetByCategorieAsync(string categorie)
        => await _productsCollection.Find(p => p.Categorie == categorie).ToListAsync().ConfigureAwait(false);

    public async Task<Product> CreateAsync(Product product)
    {
        product.DateAjout = DateTime.UtcNow;
        await _productsCollection.InsertOneAsync(product).ConfigureAwait(false);
        return product;
    }

    public async Task<bool> UpdateAsync(string id, Product product)
    {
        var result = await _productsCollection.ReplaceOneAsync(p => p.Id == id, product).ConfigureAwait(false);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _productsCollection.DeleteOneAsync(p => p.Id == id).ConfigureAwait(false);
        return result.DeletedCount > 0;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        var count = await _productsCollection.CountDocumentsAsync(p => p.Id == id).ConfigureAwait(false);
        return count > 0;
    }

    // ✅ Pagination MongoDB (côté BD)
    public async Task<PagedResult<Product>> GetPagedAsync(PagedQuery query, CancellationToken ct = default)
    {
        query.Normalize();

        var filter = Builders<Product>.Filter.Empty;

        var total = await _productsCollection.CountDocumentsAsync(filter, cancellationToken: ct);

        var items = await _productsCollection.Find(filter)
            .SortByDescending(x => x.DateAjout)
            .Skip(query.Skip)
            .Limit(query.PageSize)
            .ToListAsync(ct);

        return new PagedResult<Product>
        {
            Items = items,
            TotalCount = total,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }
}
