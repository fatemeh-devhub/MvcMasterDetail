using MasterDetailSample01.ApplicationServices.Dtos.OrderHeaderDtos;

namespace MasterDetailSample01.ApplicationServices.services.Contracts
{
    public interface IOrderHeaderApplicationService : IApplicationService
        <PostOrderHeaderDto, PutOrderHeaderDto, DeleteOrderHeaderDto, OrderHeaderDetailDto, GetAllOrderHeaderDto>
    {
    }
}
