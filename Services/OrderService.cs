using ecom_api_nosql_.Models;
using ecom_api_nosql_.MongoDb.Interface;
using ecom_api_nosql_.Services.Interface;

namespace ecom_api_nosql_.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IOrderRepository orderRepository, ILogger<OrderService> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _orderRepository.GetAllAsync();
    }

    public async Task<Order?> GetByIdAsync(string id)
    {
        return await _orderRepository.GetByIdAsync(id);
    }

    public async Task<List<Order>> GetByCustomerIdAsync(string customerId)
    {
        return await _orderRepository.GetByCustomerIdAsync(customerId);
    }

    public async Task<Order> CreateAsync(Order order)
    {
        return await _orderRepository.CreateAsync(order);
    }

    public async Task<Order?> UpdateStatutAsync(string id, string statut)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null) return null;

        order.Statut = statut;
        var updated = await _orderRepository.UpdateAsync(id, order);
        return updated ? order : null;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _orderRepository.DeleteAsync(id);
    }
}
