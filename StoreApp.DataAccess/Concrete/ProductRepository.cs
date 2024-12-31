using Microsoft.EntityFrameworkCore;
using StoreApp.DataAccess.Context;
using StoreApp.Entities.Entity;

namespace StoreApp.DataAccess.Concrete;

public class ProductRepository:GenericRepository<Product>,IProductRepository
{
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context):base(context)
    {
        _context = context;
    }

    public IQueryable<Product> GetAllProductsDetail(bool track = true)
    {
        var query = _context.Products.AsQueryable();
        if (!track)
        {
            query = query.AsNoTracking();
        }
        return query.Include(x => x.Category);
    }

    public Task<Product?> GetProductDetailByIdAsync(string id, bool track = true)
    {
        var query = _context.Products.AsQueryable();
        if (!track)
        {
            query = query.AsNoTracking();
        }
        return query.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> GetTotalProductCountAsync(bool track = false)
    {
        var query = _context.Products.AsQueryable();
        if (!track)
        {
            query = query.AsNoTracking();
        }
        return await query.CountAsync();
    }
}