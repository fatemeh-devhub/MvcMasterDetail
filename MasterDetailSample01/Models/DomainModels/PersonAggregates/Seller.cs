using MasterDetailSample01.Models.DomainModels.OrderAggregates;
using MasterDetailSample01.Models.Frameworks;

namespace MasterDetailSample01.Models.DomainModels.CustomerAggregates
{
    public class Seller : IDbSetEntity
    {
        public Guid Id { get; set; }
        public string SellerFirstName { get; set; }
        public string SellerLastName { get; set; }
        public List<OrderHeader> OrderHeader { get; set; }
        public bool IsDeleted { get; set; }
    }
}
