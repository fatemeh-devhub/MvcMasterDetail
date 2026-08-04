using MasterDetailSample01.Models.DomainModels.CustomerAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CustomerFirstName)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(x => x.CustomerLastName)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasMany(x => x.OrderHeaders)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId);

        builder.HasQueryFilter(x => !x.IsDeleted);

     
    }
}

