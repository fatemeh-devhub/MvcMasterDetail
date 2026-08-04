namespace MasterDetailSample01.ApplicationServices.Dtos.OrderDetailDtos
{
    public class PostOrderDetailDto
    {
        public Guid ParentGuid { get; set; }
        public Guid ProductId { get; set; }
       // public decimal UnitPrice { get; set; } // قیمت محصول هنگام ثبت سفارش
        public int Quantity { get; set; }
    }
}
