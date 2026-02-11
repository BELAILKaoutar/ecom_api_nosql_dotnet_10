using ecom_api_nosql_.Models;

namespace ecom_api_nosql_.Services.Interface;

public interface IOrderService
{
    Task<List<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(string id);
    Task<List<Order>> GetByCustomerIdAsync(string customerId);
    Task<Order> CreateAsync(Order order);
    Task<Order?> UpdateStatutAsync(string id, string statut);
    Task<bool> DeleteAsync(string id);
}
