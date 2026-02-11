using ecom_api_nosql_.Models;

namespace ecom_api_nosql_.MongoDb.Interface;

public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(string id);
    Task<List<Order>> GetByCustomerIdAsync(string customerId);
    Task<Order> CreateAsync(Order order);
    Task<bool> UpdateAsync(string id, Order order);
    Task<bool> DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}
