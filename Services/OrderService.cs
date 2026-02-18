using ecom_api_nosql_.Common.Pagination;
using ecom_api_nosql_.Models;
using ecom_api_nosql_.MongoDb.Interface;
using ecom_api_nosql_.Services.Interface;

namespace ecom_api_nosql_.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repo;

    public OrderService(IOrderRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Order>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Order?> GetByIdAsync(string id) => _repo.GetByIdAsync(id);
    public Task<Order> CreateAsync(Order order) => _repo.CreateAsync(order);
    public Task<Order?> UpdateStatutAsync(string id, string statut) => _repo.UpdateStatutAsync(id, statut);
    public Task<bool> DeleteAsync(string id) => _repo.DeleteAsync(id);

    // ✅ Pagination
    public Task<PagedResult<Order>> GetPagedAsync(PagedQuery query)
        => _repo.GetPagedAsync(query);
}
