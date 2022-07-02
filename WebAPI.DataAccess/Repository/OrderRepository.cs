using System.Data;
using Dapper;
using WebAPI.Domain.Entities;

namespace WebAPI.DAL.Repository;

public class OrderRepository : IRepository<Order>
{
    private readonly IDbConnection _connection;

    public OrderRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IList<Order>> GetAllAsync()
    {
        var query = @"SELECT * FROM ""Order_Get""()";
        var orders = await _connection.QueryAsync<Order, Customer, Store, Order>(query, (order, customer, store) =>
        {
            customer.Id = order.CustomerId;
            store.Id = order.StoreId;
            order.Customer = customer;
            order.Store = store;
            return order;
        }, splitOn:"FirstName,StoreName");
        return orders.ToList();
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        var query = @"SELECT * FROM ""Order_GetById""(@Id)";
        var order = await _connection.QueryAsync<Order, Customer, Store, Order>(query, (order, customer, store) =>
        {
            customer.Id = order.CustomerId;
            store.Id = order.StoreId;
            order.Customer = customer;
            order.Store = store;
            return order;
        }, new { Id = id }, splitOn:"FirstName,StoreName");
        return order.FirstOrDefault();
    }

    public async Task<int> InsertAsync(Order entity)
    {
        var query = @"SELECT ""Order_Insert""(@CustomerId, @StoreId)";
        var parameters = new DynamicParameters();
        parameters.Add("CustomerId", entity.CustomerId);
        parameters.Add("StoreId", entity.StoreId);
        var insertedId = await _connection.QuerySingleAsync<int>(query, parameters);
        return insertedId;
    }

    public async Task<int> UpdateByIdAsync(int id, Order entity)
    {
        var query = @"SELECT ""Order_UpdateById""(@Id, @CustomerId, @StoreId)";
        var parameters = new DynamicParameters();
        parameters.Add("Id", id);
        parameters.Add("CustomerId", entity.CustomerId);
        parameters.Add("StoreId", entity.StoreId);
        var affectedRowCount = await _connection.QuerySingleAsync<int>(query, parameters);
        return affectedRowCount;
    }

    public async Task<int> DeleteByIdAsync(int id)
    {
        var query = @"SELECT ""Order_DeleteById""(@Id)";
        var affectedRowCount = await _connection.QuerySingleAsync<int>(query, new { Id = id });
        return affectedRowCount;
    }
}