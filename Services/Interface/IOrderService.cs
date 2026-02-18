using ecom_api_nosql_.Models;
using ecom_api_nosql_.Common.Pagination;

namespace ecom_api_nosql_.Services.Interface;

public interface IOrderService
{
    Task<List<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(string id);
    Task<Order> CreateAsync(Order order);
    Task<Order?> UpdateStatutAsync(string id, string statut);
    Task<bool> DeleteAsync(string id);

    // ✅ Pagination
    Task<PagedResult<Order>> GetPagedAsync(PagedQuery query);
}
