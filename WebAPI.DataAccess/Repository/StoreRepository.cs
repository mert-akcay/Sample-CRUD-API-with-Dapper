using System.Data;
using Dapper;
using WebAPI.Domain.Entities;

namespace WebAPI.DAL.Repository;

public class StoreRepository : IRepository<Store>
{
    private readonly IDbConnection _connection;

    public StoreRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IList<Store>> GetAllAsync()
    {
        var query = @"SELECT * FROM ""Store_Get""()";
        var stores = await _connection.QueryAsync<Store>(query);
        return stores.ToList();
    }

    public async Task<Store> GetByIdAsync(int id)
    {
        var query = @"SELECT * FROM ""Store_GetById""(@id)";
        var store = await _connection.QuerySingleOrDefaultAsync<Store>(query, new { Id = id });
        return store;
    }

    public async Task<int> InsertAsync(Store entity)
    {
        var query = @"SELECT ""Store_Insert""(@StoreName, @Address)";
        var parameters = new DynamicParameters();
        parameters.Add("StoreName", entity.StoreName);
        parameters.Add("Address", entity.Address);
        var insertedId = await _connection.QuerySingleAsync<int>(query, parameters);
        return insertedId;
    }

    public async Task<int> UpdateByIdAsync(int id, Store entity)
    {
        var query = @"SELECT ""Store_UpdateById""(@Id, @StoreName, @Address)";
        var parameters = new DynamicParameters();
        parameters.Add("Id", id);
        parameters.Add("StoreName", entity.StoreName);
        parameters.Add("Address", entity.Address);
        var affectedRowCount = await _connection.QuerySingleAsync<int>(query, parameters);
        return affectedRowCount;
    }

    public async Task<int> DeleteByIdAsync(int id)
    {
        var query = @"SELECT ""Store_DeleteById""(@Id)";
        var affectedRowCount = await _connection.QuerySingleAsync<int>(query, new { Id = id });
        return affectedRowCount;
    }
}