using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MasterDetailSample01.Models.DomainModels.CustomerAggregates;

public class SellerConfiguration : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.ToTable("Sellers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SellerFirstName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.SellerLastName)
          .IsRequired()
          .HasMaxLength(200);

        builder.HasMany(x => x.OrderHeader)
            .WithOne(x => x.Seller)
            .HasForeignKey(x => x.SellerId);
        
        builder.HasQueryFilter(x => !x.IsDeleted);



    }
}






