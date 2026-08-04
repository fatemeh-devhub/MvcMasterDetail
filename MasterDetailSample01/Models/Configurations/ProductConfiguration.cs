using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
       
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

  
        //builder.Property(p => p.Id)
        //    .ValueGeneratedOnAdd();

    
        builder.Property(p => p.ProductName)
            .IsRequired()
            .HasMaxLength(200);
       
        builder.HasQueryFilter(x => !x.IsDeleted);
      
    }
}
