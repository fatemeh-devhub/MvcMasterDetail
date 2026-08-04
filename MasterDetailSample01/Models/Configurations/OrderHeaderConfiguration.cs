using System.Reflection.Emit;
using MasterDetailSample01.Models.DomainModels.OrderAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MasterDetailSample01.Models.Configurations
{
    public class OrderHeaderConfiguration : IEntityTypeConfiguration<OrderHeader>
    {
        public void Configure(EntityTypeBuilder<OrderHeader> builder)
        {
           builder.ToTable("OrderHeader");
            

            builder.HasKey(x => x.Id);


            builder.HasQueryFilter(x => !x.IsDeleted);

        }
    }
}
