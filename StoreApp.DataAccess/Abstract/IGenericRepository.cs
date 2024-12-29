using Microsoft.EntityFrameworkCore.Update;

namespace StoreApp.DataAccess.Abstract;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetAll(bool track = true);
    Task<T?> GetByIdAsync(string id,bool track = true);
    Task CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
