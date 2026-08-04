using MasterDetailSample01.ApplicationServices.Dtos.ProductDtos;

namespace MasterDetailSample01.ApplicationServices.services.Contracts
{
    public interface IProductApplicationService : IApplicationService
     <PostProductDto, PutProductDto, DeleteProductDto, GetByIdProductDto, GetAllProductDto>
    {
    }
}
