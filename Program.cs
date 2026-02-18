using ecom_api_nosql_.Data;
using ecom_api_nosql_.MongoDb.Interface;
using ecom_api_nosql_.MongoDb.Repository;
using ecom_api_nosql_.Services;
using ecom_api_nosql_.Services.Interface;
using ecom_api_nosql_.Settings;
using FluentValidation;
using FluentValidation.AspNetCore;
using ecom_api_nosql_.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbConfiguration>(
    builder.Configuration.GetSection("MongoDBConfiguration"));

builder.Services.AddSingleton<IMongoClientFactory, MongoClientFactory>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<ProductSeeder>();
builder.Services.AddScoped<CustomerSeeder>();
builder.Services.AddScoped<OrderSeeder>();

builder.Services.AddScoped<MongoSchemaInitializer>();

// Controllers
builder.Services.AddControllers();

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Schema + Seed
using (var scope = app.Services.CreateScope())
{
    var schema = scope.ServiceProvider.GetRequiredService<MongoSchemaInitializer>();
    await schema.EnsureAsync();

    var productSeeder = scope.ServiceProvider.GetRequiredService<ProductSeeder>();
    await productSeeder.SeedAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();
app.Run();
