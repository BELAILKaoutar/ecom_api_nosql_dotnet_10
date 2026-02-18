using ecom_api_nosql_.Models;
using ecom_api_nosql_.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using ecom_api_nosql_.Common.Pagination;
using ecom_api_nosql_.Common.Exceptions;

namespace ecom_api_nosql_.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomersController(ICustomerService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<Customer>>> GetAll([FromQuery] PagedQuery query)
        => Ok(await _service.GetPagedAsync(query));

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetById(string id)
    {
        var customer = await _service.GetByIdAsync(id);
        if (customer == null)
            throw new NotFoundException($"Customer with ID {id} not found");

        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<Customer>> Create([FromBody] Customer customer)
    {
        var created = await _service.CreateAsync(customer);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Customer>> Update(string id, [FromBody] Customer customer)
    {
        var updated = await _service.UpdateAsync(id, customer);
        if (updated == null)
            throw new NotFoundException($"Customer with ID {id} not found");

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            throw new NotFoundException($"Customer with ID {id} not found");

        return NoContent();
    }
}
