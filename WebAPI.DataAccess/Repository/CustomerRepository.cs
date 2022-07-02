using System.Data;
using Dapper;
using WebAPI.Domain.Entities;

namespace WebAPI.DAL.Repository;

public class CustomerRepository : IRepository<Customer>
{
    private readonly IDbConnection _connection;

    public CustomerRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IList<Customer>> GetAllAsync()
    {
        var query = @"SELECT * FROM ""Customer_Get""()";
        var customers = await _connection.QueryAsync<Customer>(query);
        return customers.ToList();
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        var query = @"SELECT * FROM ""Customer_GetById""(@Id)";
        var customer = await _connection.QuerySingleOrDefaultAsync<Customer>(query, new { Id = id });
        return customer;
    }

    public async Task<int> InsertAsync(Customer entity)
    {
        var query = @"SELECT ""Customer_Insert""(@FirstName, @LastName, @Phone, @Email)";
        var parameters = new DynamicParameters();
        parameters.Add("FirstName", entity.FirstName);
        parameters.Add("LastName", entity.LastName);
        parameters.Add("Phone", entity.Phone);
        parameters.Add("Email", entity.Email);
        var insertedId = await _connection.QuerySingleAsync<int>(query, parameters);
        return insertedId;
    }

    public async Task<int> UpdateByIdAsync(int id, Customer entity)
    {
        var query = @"SELECT ""Customer_UpdateById""(@Id, @FirstName, @LastName, @Phone, @Email)";
        var parameters = new DynamicParameters();
        parameters.Add("Id", id);
        parameters.Add("FirstName", entity.FirstName);
        parameters.Add("LastName", entity.LastName);
        parameters.Add("Phone", entity.Phone);
        parameters.Add("Email", entity.Email);
        var affectedRowCount = await _connection.QuerySingleAsync<int>(query, parameters);
        return affectedRowCount;
    }

    public async Task<int> DeleteByIdAsync(int id)
    {
        var query = @"SELECT ""Customer_DeleteById""(@Id)";
        var affectedRowCount = await _connection.QuerySingleAsync<int>(query, new { Id = id });
        return affectedRowCount;
    }
}