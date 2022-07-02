using System.Data;
using FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Npgsql;
using WebAPI.DAL.Repository;
using WebAPI.Domain.Entities;
using WebAPI.Service.Dtos;
using WebAPI.Service.MappingProfiles;
using WebAPI.Service.Services;
using WebAPI.Service.Validations;
using OrderRepository = WebAPI.DAL.Repository.OrderRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(builder.Configuration.GetConnectionString("PgSqlConnection")));
builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IRepository<Store>, StoreRepository>();
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IRepository<OrderItem>, OrderItemRepository>();
builder.Services.AddScoped<IService<CustomerDto>, CustomerService>();
builder.Services.AddScoped<IService<ProductDto>, ProductService>();
builder.Services.AddScoped<IService<StoreDto>, StoreService>();
builder.Services.AddScoped<IService<OrderDto>, OrderService>();
builder.Services.AddScoped<IService<OrderItemDto>, OrderItemService>();
builder.Services.AddAutoMapper(options => options.AddProfile<DtoMappingProfile>());
builder.Services.AddValidatorsFromAssemblyContaining<CustomerValidator>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swPgSqlConnectionashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationRulesToSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();