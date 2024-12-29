using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreApp.Entities.Entity;

namespace StoreApp.DataAccess.Configuration;


public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(500);

        builder.HasData(
           new Category
           {
               Id = "943b4f45-4a0f-48e2-a901-c7cc947c0a29",
               Name = "Phones",
               Description = "Mobile Phones",
               CreatedDate = DateTime.Now,
               ModifiedDate = DateTime.Now
           },
           new Category
           {
               Id = "f8d3d601-03ab-459b-9504-94851fa5d2a6",
               Name = "Laptops",
               Description = "Notebook Computers",
               CreatedDate = DateTime.Now,
               ModifiedDate = DateTime.Now
           }
       );

    }

    
}