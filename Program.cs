using ecom_api_nosql_.Data;
using ecom_api_nosql_.MongoDb.Interface;
using ecom_api_nosql_.MongoDb.Repository;
using ecom_api_nosql_.Services;
using ecom_api_nosql_.Services.Interface;
using ecom_api_nosql_.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure MongoDB settings
builder.Services.Configure<MongoDbConfiguration>(
    builder.Configuration.GetSection("MongoDBConfiguration"));

// Register MongoDB client factory
builder.Services.AddSingleton<IMongoClientFactory, MongoClientFactory>();

// Register repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Register services
builder.Services.AddScoped<IProductService, ProductService>();

// Register seeders
builder.Services.AddScoped<ProductSeeder>();

// Add controllers
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add endpoint explorer for OpenAPI
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<ProductSeeder>();
    await seeder.SeedAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Map controllers
app.MapControllers();

app.Run();