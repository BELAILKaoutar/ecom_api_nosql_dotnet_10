using ecom_api_nosql_.Models;
using ecom_api_nosql_.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using ecom_api_nosql_.Common.Pagination;
using ecom_api_nosql_.Common.Exceptions;

namespace ecom_api_nosql_.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<Order>>> GetAll([FromQuery] PagedQuery query)
        => Ok(await _orderService.GetPagedAsync(query));

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetById(string id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order == null)
            throw new NotFoundException($"Order with ID {id} not found");

        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<Order>> Create([FromBody] Order order)
    {
        var created = await _orderService.CreateAsync(order);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}/statut")]
    public async Task<ActionResult<Order>> UpdateStatut(string id, [FromBody] string statut)
    {
        var updatedOrder = await _orderService.UpdateStatutAsync(id, statut);
        if (updatedOrder == null)
            throw new NotFoundException($"Order with ID {id} not found");

        return Ok(updatedOrder);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _orderService.DeleteAsync(id);
        if (!deleted)
            throw new NotFoundException($"Order with ID {id} not found");

        return NoContent();
    }
}
