namespace MasterDetailSample01.ApplicationServices.Dtos.ProductDtos
{
    public class GetProductDto
    {
        public Guid Id { get; set; }

        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }     

    }
}
