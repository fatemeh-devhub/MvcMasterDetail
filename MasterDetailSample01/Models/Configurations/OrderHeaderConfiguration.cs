using MasterDetailSample01.Models.DomainModels.OrderAggregates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MasterDetailSample01.Models.Configurations
{
    public class OrderHeaderConfiguration : IEntityTypeConfiguration<OrderHeader>
    {
        public void Configure(EntityTypeBuilder<OrderHeader> builder)
        {
            builder.ToTable("OrderHeader");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.GuidKey)
            .IsUnique();
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
