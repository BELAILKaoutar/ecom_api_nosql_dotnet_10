using ecom_api_nosql_.Models;
using ecom_api_nosql_.MongoDb.Interface;

namespace ecom_api_nosql_.Data;

/// <summary>
/// Seeder class to populate the Products collection with initial data
/// </summary>
public class ProductSeeder
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductSeeder> _logger;

    public ProductSeeder(IProductRepository productRepository, ILogger<ProductSeeder> logger)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Seeds the database with initial product data
    /// </summary>
    public async Task SeedAsync()
    {
        try
        {
            _logger.LogInformation("Starting product seeding...");

            // Check if products already exist
            var existingProducts = await _productRepository.GetAllAsync();
            if (existingProducts.Any())
            {
                _logger.LogInformation("Products already exist. Skipping seed.");
                return;
            }

            var products = GetSeedProducts();

            foreach (var product in products)
            {
                await _productRepository.CreateAsync(product);
                _logger.LogInformation("Seeded product: {ProductName}", product.Nom);
            }

            _logger.LogInformation("Product seeding completed. {Count} products added.", products.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while seeding products");
            throw;
        }
    }

    /// <summary>
    /// Gets the list of seed products
    /// </summary>
    /// <returns>List of products to seed</returns>
    private List<Product> GetSeedProducts()
    {
        return new List<Product>
        {
            new Product
            {
                Nom = "Laptop Dell XPS 15",
                Categorie = "Informatique",
                Prix = 1299.99m,
                Stock = 25,
                Specifications = new ProductSpecifications
                {
                    Processeur = "Intel i7",
                    Ram = "16GB",
                    Stockage = "512GB SSD"
                },
                Tags = new List<string> { "ordinateur", "portable", "gaming" },
                DateAjout = DateTime.UtcNow
            },
            new Product
            {
                Nom = "MacBook Pro 16",
                Categorie = "Informatique",
                Prix = 2499.99m,
                Stock = 15,
                Specifications = new ProductSpecifications
                {
                    Processeur = "Apple M2 Pro",
                    Ram = "32GB",
                    Stockage = "1TB SSD"
                },
                Tags = new List<string> { "ordinateur", "portable", "professionnel" },
                DateAjout = DateTime.UtcNow
            },
            new Product
            {
                Nom = "HP Pavilion Gaming",
                Categorie = "Informatique",
                Prix = 899.99m,
                Stock = 30,
                Specifications = new ProductSpecifications
                {
                    Processeur = "AMD Ryzen 7",
                    Ram = "16GB",
                    Stockage = "512GB SSD"
                },
                Tags = new List<string> { "ordinateur", "portable", "gaming", "budget" },
                DateAjout = DateTime.UtcNow
            },
            new Product
            {
                Nom = "Lenovo ThinkPad X1",
                Categorie = "Informatique",
                Prix = 1599.99m,
                Stock = 20,
                Specifications = new ProductSpecifications
                {
                    Processeur = "Intel i7",
                    Ram = "16GB",
                    Stockage = "512GB SSD"
                },
                Tags = new List<string> { "ordinateur", "portable", "professionnel", "business" },
                DateAjout = DateTime.UtcNow
            },
            new Product
            {
                Nom = "ASUS ROG Strix",
                Categorie = "Informatique",
                Prix = 1899.99m,
                Stock = 12,
                Specifications = new ProductSpecifications
                {
                    Processeur = "Intel i9",
                    Ram = "32GB",
                    Stockage = "1TB SSD"
                },
                Tags = new List<string> { "ordinateur", "portable", "gaming", "high-end" },
                DateAjout = DateTime.UtcNow
            },
            new Product
            {
                Nom = "Samsung Galaxy Book",
                Categorie = "Informatique",
                Prix = 749.99m,
                Stock = 18,
                Specifications = new ProductSpecifications
                {
                    Processeur = "Intel i5",
                    Ram = "8GB",
                    Stockage = "256GB SSD"
                },
                Tags = new List<string> { "ordinateur", "portable", "leger" },
                DateAjout = DateTime.UtcNow
            },
            new Product
            {
                Nom = "Microsoft Surface Laptop",
                Categorie = "Informatique",
                Prix = 1199.99m,
                Stock = 22,
                Specifications = new ProductSpecifications
                {
                    Processeur = "Intel i7",
                    Ram = "16GB",
                    Stockage = "512GB SSD"
                },
                Tags = new List<string> { "ordinateur", "portable", "design" },
                DateAjout = DateTime.UtcNow
            },
            new Product
            {
                Nom = "Acer Aspire 5",
                Categorie = "Informatique",
                Prix = 549.99m,
                Stock = 40,
                Specifications = new ProductSpecifications
                {
                    Processeur = "AMD Ryzen 5",
                    Ram = "8GB",
                    Stockage = "256GB SSD"
                },
                Tags = new List<string> { "ordinateur", "portable", "budget", "etudiant" },
                DateAjout = DateTime.UtcNow
            }
        };
    }
}
