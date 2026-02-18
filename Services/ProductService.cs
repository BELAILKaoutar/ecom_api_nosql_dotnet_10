using ecom_api_nosql_.Models;
using ecom_api_nosql_.MongoDb.Interface;
using ecom_api_nosql_.Services.Interface;
using ecom_api_nosql_.Common.Pagination;

namespace ecom_api_nosql_.Services;

/// <summary>
/// Service implementation for Product operations
/// </summary>
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task<List<Product>> GetAllProductsAsync()
    {
        try
        {
            _logger.LogInformation("Retrieving all products");
            return await _productRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all products");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<Product?> GetProductByIdAsync(string id)
    {
        try
        {
            _logger.LogInformation("Retrieving product with ID: {ProductId}", id);
            return await _productRepository.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving product with ID: {ProductId}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<List<Product>> GetProductsByCategorieAsync(string categorie)
    {
        try
        {
            _logger.LogInformation("Retrieving products in category: {Category}", categorie);
            return await _productRepository.GetByCategorieAsync(categorie);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving products in category: {Category}", categorie);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<Product> CreateProductAsync(Product product)
    {
        try
        {
            _logger.LogInformation("Creating new product: {ProductName}", product.Nom);
            
            // Validate product data
            ValidateProduct(product);
            
            return await _productRepository.CreateAsync(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating product: {ProductName}", product.Nom);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<Product?> UpdateProductAsync(string id, Product product)
    {
        try
        {
            _logger.LogInformation("Updating product with ID: {ProductId}", id);
            
            // Check if product exists
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                _logger.LogWarning("Product with ID: {ProductId} not found", id);
                return null;
            }

            // Validate product data
            ValidateProduct(product);
            
            product.Id = id;
            var updated = await _productRepository.UpdateAsync(id, product);
            
            return updated ? product : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating product with ID: {ProductId}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> DeleteProductAsync(string id)
    {
        try
        {
            _logger.LogInformation("Deleting product with ID: {ProductId}", id);
            
            // Check if product exists
            var exists = await _productRepository.ExistsAsync(id);
            if (!exists)
            {
                _logger.LogWarning("Product with ID: {ProductId} not found", id);
                return false;
            }

            return await _productRepository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting product with ID: {ProductId}", id);
            throw;
        }
    }

    /// <summary>
    /// Validates product data
    /// </summary>
    /// <param name="product">Product to validate</param>
    /// <exception cref="ArgumentException">Thrown when validation fails</exception>
    private void ValidateProduct(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Nom))
        {
            throw new ArgumentException("Product name is required", nameof(product.Nom));
        }

        if (string.IsNullOrWhiteSpace(product.Categorie))
        {
            throw new ArgumentException("Product category is required", nameof(product.Categorie));
        }

        if (product.Prix <= 0)
        {
            throw new ArgumentException("Product price must be greater than zero", nameof(product.Prix));
        }

        if (product.Stock < 0)
        {
            throw new ArgumentException("Product stock cannot be negative", nameof(product.Stock));
        }
    }
    public Task<PagedResult<Product>> GetPagedAsync(PagedQuery query)
    => _productRepository.GetPagedAsync(query);


}
