using ecom_api_nosql_.Models;
using ecom_api_nosql_.Common.Pagination;

namespace ecom_api_nosql_.MongoDb.Interface;

/// <summary>
/// Interface for Product repository operations
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Gets all products
    /// </summary>
    /// <returns>List of all products</returns>
    Task<List<Product>> GetAllAsync();

    /// <summary>
    /// Gets a product by its ID
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>Product if found, null otherwise</returns>
    Task<Product?> GetByIdAsync(string id);

    /// <summary>
    /// Gets products by category
    /// </summary>
    /// <param name="categorie">Category name</param>
    /// <returns>List of products in the specified category</returns>
    Task<List<Product>> GetByCategorieAsync(string categorie);

    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="product">Product to create</param>
    /// <returns>The created product</returns>
    Task<Product> CreateAsync(Product product);

    /// <summary>
    /// Updates an existing product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="product">Updated product data</param>
    /// <returns>True if update was successful, false otherwise</returns>
    Task<bool> UpdateAsync(string id, Product product);

    /// <summary>
    /// Deletes a product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>True if deletion was successful, false otherwise</returns>
    Task<bool> DeleteAsync(string id);

    /// <summary>
    /// Checks if a product exists
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>True if product exists, false otherwise</returns>
    Task<bool> ExistsAsync(string id);

    Task<PagedResult<Product>> GetPagedAsync(PagedQuery query, CancellationToken ct = default);

}
