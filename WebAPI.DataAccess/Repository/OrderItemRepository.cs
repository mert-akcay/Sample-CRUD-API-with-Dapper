using System.Data;
using Dapper;
using WebAPI.Domain.Entities;

namespace WebAPI.DAL.Repository;

public class OrderItemRepository : IRepository<OrderItem>
{
    private readonly IDbConnection _connection;

    public OrderItemRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IList<OrderItem>> GetAllAsync()
    {
        var query = @"SELECT * FROM ""OrderItem_Get""()";
        var orderItems = await _connection.QueryAsync<OrderItem,Order,Customer,Store,Product,OrderItem>(query, (orderItem,order,customer,store,product) =>
        {
            order.Id = orderItem.OrderId;
            product.Id = orderItem.ProductId;
            customer.Id = order.CustomerId;
            store.Id = order.StoreId;
            orderItem.Order = order;
            orderItem.Product = product;
            orderItem.Order.Customer = customer;
            orderItem.Order.Store = store;
            return orderItem;
        }, splitOn: "CustomerId,FirstName,StoreName,Name");
        return orderItems.ToList();
    }

    public async Task<OrderItem> GetByIdAsync(int id)
    {
        var query = @"SELECT * FROM ""OrderItem_GetById""(@id)";
        var orderItem = await _connection.QueryAsync<OrderItem,Order,Customer,Store,Product,OrderItem>(query,(orderItem,order,customer,store,product) =>
        {
            order.Id = orderItem.OrderId;
            product.Id = orderItem.ProductId;
            customer.Id = order.CustomerId;
            store.Id = order.StoreId;
            orderItem.Order = order;
            orderItem.Product = product;
            orderItem.Order.Customer = customer;
            orderItem.Order.Store = store;
            return orderItem;
        } , new { Id = id }, splitOn: "CustomerId,FirstName,StoreName,Name");
        return orderItem.FirstOrDefault();
    }

    public async Task<int> InsertAsync(OrderItem entity)
    {
        var query = @"SELECT ""OrderItem_Insert""(@OrderId, @ProductId, @Quantity)";
        var parameters = new DynamicParameters();
        parameters.Add("@OrderId", entity.OrderId);
        parameters.Add("@ProductId", entity.ProductId);
        parameters.Add("@Quantity", entity.Quantity);
        var insertedId = await _connection.QuerySingleAsync<int>(query, parameters);
        return insertedId;
    }

    public async Task<int> UpdateByIdAsync(int id, OrderItem entity)
    {
        var query = @"SELECT ""OrderItem_UpdateById""(@Id, @OrderId, @ProductId, @Quantity)";
        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        parameters.Add("@OrderId", entity.OrderId);
        parameters.Add("@ProductId", entity.ProductId);
        parameters.Add("@Quantity", entity.Quantity);
        var affectedRowCount = await _connection.QuerySingleAsync<int>(query, parameters);
        return affectedRowCount;
    }

    public async Task<int> DeleteByIdAsync(int id)
    {
        var query = @"SELECT ""OrderItem_DeleteById""(@id)";
        var affectedRowCount = await _connection.QuerySingleAsync<int>(query, new { Id = id });
        return affectedRowCount;
    }
}