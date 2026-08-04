using MasterDetailSample01.ApplicationServices.Dtos.SellerDtos;

namespace MasterDetailSample01.ApplicationServices.services.Contracts
{
    public interface ISellerApplicationService : IApplicationService
        <PostSellerDto,PutSellerDto,DeleteSellerDto,GetByIdSellerDto,GetAllSellerDto>
    {

    }
}
