using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Entities.Entity;

namespace StoreApp.DataAccess.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Price)
       .HasPrecision(18,2)
       .IsRequired();
        builder.Property(x => x.Stock).IsRequired();
        builder.Property(x => x.ImageUrl).HasMaxLength(250);
        builder.Property(x => x.Description).HasMaxLength(500);

         builder.HasData(
           new Product
           {
               Id = Guid.NewGuid().ToString(),
               Name = "iPhone 14",
               Description = "Apple iPhone 14 128GB",
               Price = 999.99M,
               Stock = 100,
               ImageUrl = "images/iphone14.jpg",
               CategoryId = "943b4f45-4a0f-48e2-a901-c7cc947c0a29",
               CreatedDate = DateTime.Now,
               ModifiedDate = DateTime.Now
           },
           new Product
           {
               Id = Guid.NewGuid().ToString(), 
               Name = "Samsung Galaxy S23",
               Description = "Samsung Galaxy S23 256GB",
               Price = 899.99M,
               Stock = 75,
               ImageUrl = "images/galaxys23.jpg",
               CategoryId = "f8d3d601-03ab-459b-9504-94851fa5d2a6",
               CreatedDate = DateTime.Now,
               ModifiedDate = DateTime.Now
           }
       );
    }
}
