using StoreApp.DataAccess.Abstract;
using StoreApp.DataAccess.Concrete;
using StoreApp.DataAccess.Context;

namespace StoreApp.DataAccess.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private CategoryRepository? _categoryRepository;
    private ProductRepository? _productRepository;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);

    public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);
    public ValueTask DisposeAsync()
    {
        return _context.DisposeAsync();
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}