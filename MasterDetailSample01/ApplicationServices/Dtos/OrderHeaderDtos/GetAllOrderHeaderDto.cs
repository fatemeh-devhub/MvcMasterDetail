using MasterDetailSample01.ApplicationServices.Dtos.OrderDetailDtos;

namespace MasterDetailSample01.ApplicationServices.Dtos.OrderHeaderDtos

{
    public class GetAllOrderHeaderDto
    {
        public Guid Id { get; set; }
     
        public Guid GuidKey { get; set; }

        public Guid CustomerId { get; set; }

        public string CustomerFirstName{ get; set; }
       
        public string CustomerLastName { get; set; }

        public Guid SellerId { get; set; }

        public string SellerFirstName { get; set; }
        
        public string SellerLastName { get; set; }

       public decimal FinalPrice => OrderDetails?.Sum(x => x.TotalPrice) ?? 0;

        public List<GetAllOrderDetailDto> OrderDetails { get; set; }

       
    }
}
