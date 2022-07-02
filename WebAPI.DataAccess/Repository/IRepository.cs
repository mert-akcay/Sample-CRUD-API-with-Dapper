using WebAPI.Domain.Entities;

namespace WebAPI.DAL.Repository;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    public Task<IList<TEntity>> GetAllAsync();
    public Task<TEntity> GetByIdAsync(int id);
    public Task<int> InsertAsync(TEntity entity);
    public Task<int> UpdateByIdAsync(int id, TEntity entity);
    public Task<int> DeleteByIdAsync(int id);
}