using ecom_api_nosql_.Models;

namespace ecom_api_nosql_.Services.Interface;

/// <summary>
/// Interface for Product service operations
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Gets all products
    /// </summary>
    /// <returns>List of all products</returns>
    Task<List<Product>> GetAllProductsAsync();

    /// <summary>
    /// Gets a product by its ID
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>Product if found, null otherwise</returns>
    Task<Product?> GetProductByIdAsync(string id);

    /// <summary>
    /// Gets products by category
    /// </summary>
    /// <param name="categorie">Category name</param>
    /// <returns>List of products in the specified category</returns>
    Task<List<Product>> GetProductsByCategorieAsync(string categorie);

    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="product">Product to create</param>
    /// <returns>The created product</returns>
    Task<Product> CreateProductAsync(Product product);

    /// <summary>
    /// Updates an existing product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="product">Updated product data</param>
    /// <returns>The updated product if successful, null otherwise</returns>
    Task<Product?> UpdateProductAsync(string id, Product product);

    /// <summary>
    /// Deletes a product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>True if deletion was successful, false otherwise</returns>
    Task<bool> DeleteProductAsync(string id);
}
