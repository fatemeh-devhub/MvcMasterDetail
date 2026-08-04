using MasterDetailSample01.Models.DomainModels.OrderAggregates;
using MasterDetailSample01.Models.Frameworks;


namespace MasterDetailSample01.Models.DomainModels.CustomerAggregates
{
    public class Customer:IDbSetEntity
    {
        public Guid Id { get; set; }

        public string CustomerFirstName { get; set; }
        
        public string CustomerLastName { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<OrderHeader> OrderHeaders { get; set; }

        public bool IsDeleted { get; set; }
    }
}
