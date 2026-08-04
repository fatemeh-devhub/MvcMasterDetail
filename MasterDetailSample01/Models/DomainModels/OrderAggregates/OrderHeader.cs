using MasterDetailSample01.Models.DomainModels.CustomerAggregates;
using MasterDetailSample01.Models.Frameworks;


namespace MasterDetailSample01.Models.DomainModels.OrderAggregates
{
    public class OrderHeader:IDbSetEntity
    {
        public Guid Id { get; set; }
        public Guid GuidKey { get; set; }
        public Guid CustomerId { get; set; }
        public Guid SellerId { get; set; }
        public decimal TotalPrice { get; set; }
        public Seller Seller { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } 
        public bool IsDeleted { get; set; } 
    }
}
