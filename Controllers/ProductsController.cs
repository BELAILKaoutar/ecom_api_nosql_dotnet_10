using ecom_api_nosql_.Models;
using ecom_api_nosql_.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using ecom_api_nosql_.Common.Pagination;
using ecom_api_nosql_.Common.Exceptions;

namespace ecom_api_nosql_.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<Product>>> GetAllProducts([FromQuery] PagedQuery query)
        => Ok(await _productService.GetPagedAsync(query));

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(string id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
            throw new NotFoundException($"Product with ID {id} not found");

        return Ok(product);
    }

    [HttpGet("category/{categorie}")]
    public async Task<ActionResult<List<Product>>> GetProductsByCategory(string categorie)
        => Ok(await _productService.GetProductsByCategorieAsync(categorie));

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
        var createdProduct = await _productService.CreateProductAsync(product);
        return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> UpdateProduct(string id, [FromBody] Product product)
    {
        var updatedProduct = await _productService.UpdateProductAsync(id, product);
        if (updatedProduct == null)
            throw new NotFoundException($"Product with ID {id} not found");

        return Ok(updatedProduct);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        var deleted = await _productService.DeleteProductAsync(id);
        if (!deleted)
            throw new NotFoundException($"Product with ID {id} not found");

        return NoContent();
    }
}
