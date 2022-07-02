using System.Data;
using Dapper;
using WebAPI.Domain.Entities;

namespace WebAPI.DAL.Repository;

public class ProductRepository : IRepository<Product>
{
    private readonly IDbConnection _connection;

    public ProductRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IList<Product>> GetAllAsync()
    {
        var query = @"SELECT * FROM ""Product_Get""()";
        var products = await _connection.QueryAsync<Product>(query);
        return products.ToList();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        var query = @"SELECT * FROM ""Product_GetById""(@id)";
        var product = await _connection.QuerySingleOrDefaultAsync<Product>(query, new {Id = id});
        return product;
    }

    public async Task<int> InsertAsync(Product entity)
    {
        var query = @"SELECT * FROM ""Product_Insert""(@Name, @UnitPrice)";
        var parameters = new DynamicParameters();
        parameters.Add("Name", entity.Name);
        parameters.Add("UnitPrice", entity.UnitPrice);
        var insertedId = await _connection.QuerySingleAsync<int>(query, parameters);
        return insertedId;
    }

    public async Task<int> UpdateByIdAsync(int id, Product entity)
    {
        var query = @"SELECT * FROM ""Product_UpdateById""(@Id, @Name, @UnitPrice)";
        var parameters = new DynamicParameters();
        parameters.Add("Id", id);
        parameters.Add("Name", entity.Name);
        parameters.Add("UnitPrice", entity.UnitPrice);
        var affectedRowCount = await _connection.QuerySingleAsync(query, parameters);
        return affectedRowCount;
    }

    public async Task<int> DeleteByIdAsync(int id)
    {
        var query = @"SELECT * FROM ""Product_DeleteById""(@Id)";
        var affectedRowCount = await _connection.QuerySingleAsync(query, new {Id = id});
        return affectedRowCount;

    }
}