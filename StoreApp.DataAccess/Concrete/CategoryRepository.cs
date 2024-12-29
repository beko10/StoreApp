using StoreApp.DataAccess.Abstract;
using StoreApp.DataAccess.Context;
using StoreApp.Entities.Entity;

namespace StoreApp.DataAccess.Concrete;

public class CategoryRepository:GenericRepository<Category>,ICategoryRepository
{
    public CategoryRepository(AppDbContext context):base(context)
    {
    }
}