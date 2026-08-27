
namespace MasterDetailSample01.ApplicationServices.Dtos.OrderDetailDtos
{
    public class PostOrderDetailDto
    {
        public Guid ParentGuid { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
