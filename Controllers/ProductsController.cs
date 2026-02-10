using ecom_api_nosql_.Models;
using ecom_api_nosql_.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ecom_api_nosql_.Controllers;

/// <summary>
/// REST API Controller for Product operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets all products
    /// </summary>
    /// <returns>List of all products</returns>
    /// <response code="200">Returns the list of products</response>
    /// <response code="500">Internal server error</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<Product>>> GetAllProducts()
    {
        try
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all products");
            return StatusCode(500, "An error occurred while retrieving products");
        }
    }

    /// <summary>
    /// Gets a product by ID
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>The requested product</returns>
    /// <response code="200">Returns the product</response>
    /// <response code="404">Product not found</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Product>> GetProductById(string id)
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(id);
            
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found");
            }

            return Ok(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving product with ID: {ProductId}", id);
            return StatusCode(500, "An error occurred while retrieving the product");
        }
    }

    /// <summary>
    /// Gets products by category
    /// </summary>
    /// <param name="categorie">Category name</param>
    /// <returns>List of products in the specified category</returns>
    /// <response code="200">Returns the list of products</response>
    /// <response code="500">Internal server error</response>
    [HttpGet("category/{categorie}")]
    [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<Product>>> GetProductsByCategory(string categorie)
    {
        try
        {
            var products = await _productService.GetProductsByCategorieAsync(categorie);
            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving products in category: {Category}", categorie);
            return StatusCode(500, "An error occurred while retrieving products");
        }
    }

    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="product">Product to create</param>
    /// <returns>The created product</returns>
    /// <response code="201">Product created successfully</response>
    /// <response code="400">Invalid product data</response>
    /// <response code="500">Internal server error</response>
    [HttpPost]
    [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
        try
        {
            if (product == null)
            {
                return BadRequest("Product data is required");
            }

            var createdProduct = await _productService.CreateProductAsync(product);
            
            return CreatedAtAction(
                nameof(GetProductById), 
                new { id = createdProduct.Id }, 
                createdProduct);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid product data");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating product");
            return StatusCode(500, "An error occurred while creating the product");
        }
    }

    /// <summary>
    /// Updates an existing product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="product">Updated product data</param>
    /// <returns>The updated product</returns>
    /// <response code="200">Product updated successfully</response>
    /// <response code="400">Invalid product data</response>
    /// <response code="404">Product not found</response>
    /// <response code="500">Internal server error</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Product>> UpdateProduct(string id, [FromBody] Product product)
    {
        try
        {
            if (product == null)
            {
                return BadRequest("Product data is required");
            }

            var updatedProduct = await _productService.UpdateProductAsync(id, product);
            
            if (updatedProduct == null)
            {
                return NotFound($"Product with ID {id} not found");
            }

            return Ok(updatedProduct);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid product data");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating product with ID: {ProductId}", id);
            return StatusCode(500, "An error occurred while updating the product");
        }
    }

    /// <summary>
    /// Deletes a product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>No content</returns>
    /// <response code="204">Product deleted successfully</response>
    /// <response code="404">Product not found</response>
    /// <response code="500">Internal server error</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        try
        {
            var deleted = await _productService.DeleteProductAsync(id);
            
            if (!deleted)
            {
                return NotFound($"Product with ID {id} not found");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting product with ID: {ProductId}", id);
            return StatusCode(500, "An error occurred while deleting the product");
        }
    }
}
