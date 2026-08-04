using MasterDetailSample01.ApplicationServices.Dtos.OrderDetailDtos;

namespace MasterDetailSample01.ApplicationServices.Dtos.OrderHeaderDtos
{
    public class PutOrderHeaderDto
    {
            public Guid Id { get; set; }
      
            public Guid GuidKey { get; set; }
            
            public Guid CustomerId { get; set; }

            public Guid SellerId { get; set; }
           
            public List<PutOrderDetailDto> OrderDetails { get; set; }
    }
}
