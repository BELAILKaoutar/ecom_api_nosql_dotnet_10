using ecom_api_nosql_.Models;
using ecom_api_nosql_.MongoDb.Interface;

namespace ecom_api_nosql_.Data;

public class OrderSeeder
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderSeeder> _logger;

    public OrderSeeder(IOrderRepository orderRepository, ILogger<OrderSeeder> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        var existing = await _orderRepository.GetAllAsync();
        if (existing.Any()) return;

        var orders = new List<Order>
        {
            new Order
            {
                CustomerId = "6989ef7ae45ac59a4bf3a04a",
                DateCommande = DateTime.UtcNow,
                Statut = "livrée",
                Articles = new List<string>{ "Laptop Dell XPS 15" },
                MontantTotal = 1299.99m,
                AdresseLivraison = "10 rue de la Liberté, 75001 Paris"
            }
        };

        foreach (var order in orders)
        {
            await _orderRepository.CreateAsync(order);
            _logger.LogInformation("Seeded order for customer {CustomerId}", order.CustomerId);
        }
    }
}
