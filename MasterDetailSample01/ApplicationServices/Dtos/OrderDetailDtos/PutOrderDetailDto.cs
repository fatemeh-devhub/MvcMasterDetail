namespace MasterDetailSample01.ApplicationServices.Dtos.OrderDetailDtos
{
    public class PutOrderDetailDto
    {
        public Guid Id { get; set; }
        public Guid ParentGuid { get; set; }
       public Guid ProductId { get; set; }
       public decimal UnitPrice { get; set; }  
       public int Quantity { get; set; }
    }
}
