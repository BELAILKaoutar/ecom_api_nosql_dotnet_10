using ecom_api_nosql_.Models;
using ecom_api_nosql_.MongoDb.Interface;
using ecom_api_nosql_.Services.Interface;

namespace ecom_api_nosql_.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer?> GetByIdAsync(string id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            customer.DateCreation = DateTime.UtcNow;
            return await _customerRepository.CreateAsync(customer);
        }

        public async Task<Customer?> UpdateAsync(string id, Customer customer)
        {
            var exists = await _customerRepository.ExistsAsync(id);
            if (!exists) return null;

            customer.Id = id;
            var updated = await _customerRepository.UpdateAsync(id, customer);
            return updated ? customer : null;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _customerRepository.DeleteAsync(id);
        }
    }
}
