using MasterDetailSample01.ApplicationServices.Dtos.CustomerDtos;

namespace MasterDetailSample01.ApplicationServices.services.Contracts
{
    public interface ICustomerApplicationService : IApplicationService
        <PostCustomerDto,PutCustomerDto, DeleteCustomerDto, GetByIdCustomerDto,GetAllCustomerDto>
    {
    }
}
