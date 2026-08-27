using MasterDetailSample01.Models.Frameworks;


namespace MasterDetailSample01.Models.DomainModels.OrderAggregates
{
    public class OrderDetail:IDbSetEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ParentGuid { get; set; }
        public Guid ProductId { get; set; }
        public decimal UnitPrice { get; set; } 
        public int Quantity { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public Product Product { get; set; }
        public bool IsDeleted { get; set; }
    }
}

