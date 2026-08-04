using MasterDetailSample01.Models.DomainModels.OrderAggregates;

namespace MasterDetailSample01.ApplicationServices.Dtos.SellerDtos
{
    public class GetAllSellerDto
    {
        public Guid Id { get; set; }
        public string SellerFirstName { get; set; }
        public string SellerLastName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
