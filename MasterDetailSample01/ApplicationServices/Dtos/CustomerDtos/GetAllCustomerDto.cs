namespace MasterDetailSample01.ApplicationServices.Dtos.CustomerDtos
{
    public class GetAllCustomerDto
    {
        public Guid Id { get; set; }

        public string CustomerFirstName{ get; set; }
        public string CustomerLastName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
