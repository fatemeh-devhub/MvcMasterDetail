using MasterDetailSample01.Models.DomainModels.CustomerAggregates;
using MasterDetailSample01.Models.DomainModels.OrderAggregates;

namespace MasterDetailSample01.ApplicationServices.Dtos.OrderHeaderDtos
{
    public class OrderHeaderDetailDto
    {
        public Guid Id { get; set; }

        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }

        public string SellerFirstName { get; set; }
        public string SellerLastName { get; set; }

        public Seller Seller { get; set; }

        public Customer Customer { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
