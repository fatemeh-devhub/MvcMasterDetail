using MasterDetailSample01.Models.DomainModels.CustomerAggregates;

namespace MasterDetailSample01.Models.Services.Contracts
{
    public interface ICustomerRepository : IRepository<Customer>
    {
    }
}
