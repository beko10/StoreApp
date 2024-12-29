using StoreApp.DataAccess.Abstract;
using StoreApp.Entities.Entity;

namespace StoreApp.DataAccess.Concrete;

public interface IProductRepository:IGenericRepository<Product>
{
    Task<int> GetTotalProductCountAsync(bool track = false);
}