
using MasterDetailSample01.ApplicationServices.Dtos.OrderDetailDtos;


namespace MasterDetailSample01.ApplicationServices.Dtos.OrderHeaderDtos
{
   
        public class PostOrderHeaderDto
        {
            public Guid GuidKey { get; set; }
            public Guid CustomerId { get; set; }

            public Guid SellerId { get; set; }

           // public decimal TotalPrice { get; set; }

           //public decimal TotalPrice => OrderDetails.Sum(x => x.UnitPrice * x.Quantity);
           public List<PostOrderDetailDto> OrderDetails { get; set; }

        }

    
}
