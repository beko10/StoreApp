using Microsoft.EntityFrameworkCore;
using StoreApp.DataAccess.Abstract;
using StoreApp.DataAccess.Context;
using StoreApp.Entities.Entity;

namespace StoreApp.DataAccess.Concrete;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{

    private readonly AppDbContext _context;
    private DbSet<T> _dbSet => _context.Set<T>();

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> GetAll(bool track = true)
    {
        return track ? _dbSet : _dbSet.AsNoTracking();
    }

    public async Task<T?> GetByIdAsync(string id, bool track = true)
    {
        var query = _dbSet.AsQueryable();
        if (!track)
        {
            query = query.AsNoTracking();
        }
        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);  
    }
}
