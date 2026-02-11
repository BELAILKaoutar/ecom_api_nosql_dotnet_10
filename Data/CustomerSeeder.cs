using ecom_api_nosql_.Models;
using ecom_api_nosql_.MongoDb.Interface;

namespace ecom_api_nosql_.Data
{
    public class CustomerSeeder
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerSeeder> _logger;

        public CustomerSeeder(ICustomerRepository customerRepository, ILogger<CustomerSeeder> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            var existing = await _customerRepository.GetAllAsync();
            if (existing.Any()) return;

            var customers = new List<Customer>
            {
                new Customer
                {
                    Nom = "Martin",
                    Prenom = "Jean",
                    Email = "jean.martin@email.com",
                    Telephone = "0623456789",
                    Adresse = "10 rue de Paris, 75001 Paris",
                    DateCreation = DateTime.Parse("2023-03-20"),
                    Statut = CustomerStatus.Premium
                },
                new Customer
                {
                    Nom = "Dupont",
                    Prenom = "Alice",
                    Email = "alice.dupont@email.com",
                    Telephone = "0612345678",
                    Adresse = "5 avenue de Lyon, 69000 Lyon",
                    DateCreation = DateTime.Parse("2023-05-15"),
                    Statut = CustomerStatus.Standard
                }
            };

            foreach (var c in customers)
            {
                await _customerRepository.CreateAsync(c);
            }

            _logger.LogInformation("{Count} customers seeded.", customers.Count);
        }
    }
}
