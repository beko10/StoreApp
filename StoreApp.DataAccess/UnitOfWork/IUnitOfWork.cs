using StoreApp.DataAccess.Abstract;
using StoreApp.DataAccess.Concrete;

namespace StoreApp.DataAccess.UnitOfWork;

public interface IUnitOfWork:IAsyncDisposable
{
    ICategoryRepository CategoryRepository { get; }
    IProductRepository ProductRepository { get; }
    Task<int> SaveAsync();
}