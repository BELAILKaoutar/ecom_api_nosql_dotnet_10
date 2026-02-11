using ecom_api_nosql_.Data;
using ecom_api_nosql_.MongoDb.Interface;
using ecom_api_nosql_.MongoDb.Repository;
using ecom_api_nosql_.Services;
using ecom_api_nosql_.Services.Interface;
using ecom_api_nosql_.Settings;


var builder = WebApplication.CreateBuilder(args);

// MongoDB config
builder.Services.Configure<MongoDbConfiguration>(
    builder.Configuration.GetSection("MongoDBConfiguration"));

// Mongo client
builder.Services.AddSingleton<IMongoClientFactory, MongoClientFactory>();

// Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Seeder
builder.Services.AddScoped<ProductSeeder>();
builder.Services.AddScoped<CustomerSeeder>();
builder.Services.AddScoped<OrderSeeder>();
// Controllers
builder.Services.AddControllers();
    
// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed DB
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<ProductSeeder>();
    await seeder.SeedAsync();
}

// Pipeline
// Pipeline'
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
