using ecom_api_nosql_.Models;

namespace ecom_api_nosql_.MongoDb.Interface;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(string id);
    Task<Customer> CreateAsync(Customer customer);
    Task<bool> UpdateAsync(string id, Customer customer);
    Task<bool> DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}
