using MasterDetailSample01.Models.DomainModels.OrderAggregates;
using MasterDetailSample01.Models.Frameworks;

public class Product : IDbSetEntity
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }    
    public List<OrderDetail> OrderDetails { get; set; }
    public bool IsDeleted { get; set; }
}