using ecom_api_nosql_.Common.Pagination;
using ecom_api_nosql_.Models;
using ecom_api_nosql_.MongoDb.Interface;
using ecom_api_nosql_.Services.Interface;

namespace ecom_api_nosql_.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repo;

    public CustomerService(ICustomerRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Customer>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Customer?> GetByIdAsync(string id) => _repo.GetByIdAsync(id);
    public Task<Customer> CreateAsync(Customer customer) => _repo.CreateAsync(customer);
    public Task<Customer?> UpdateAsync(string id, Customer customer) => _repo.UpdateAsync(id, customer);
    public Task<bool> DeleteAsync(string id) => _repo.DeleteAsync(id);

    // ✅ Pagination
    public Task<PagedResult<Customer>> GetPagedAsync(PagedQuery query)
        => _repo.GetPagedAsync(query);
}
