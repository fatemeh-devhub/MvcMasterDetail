namespace MasterDetailSample01.ApplicationServices.Dtos.OrderDetailDtos
{
    public class GetAllOrderDetailDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; } // قیمت در دیتابیس موقع ثبت سفارش نشسته
        public int Quantity { get; set; }

        public decimal TotalPrice => UnitPrice * Quantity;

    }
}
