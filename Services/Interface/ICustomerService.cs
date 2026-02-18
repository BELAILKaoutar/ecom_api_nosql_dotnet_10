using ecom_api_nosql_.Models;
using ecom_api_nosql_.Common.Pagination;

namespace ecom_api_nosql_.Services.Interface;

public interface ICustomerService
{
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(string id);
    Task<Customer> CreateAsync(Customer customer);
    Task<Customer?> UpdateAsync(string id, Customer customer);
    Task<bool> DeleteAsync(string id);

    // ✅ Pagination
    Task<PagedResult<Customer>> GetPagedAsync(PagedQuery query);
}
