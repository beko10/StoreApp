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