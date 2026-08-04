
using System.Net;
using MasterDetailSample01.ApplicationServices.Dtos.ProductDtos;
using MasterDetailSample01.ApplicationServices.services.Contracts;
using MasterDetailSample01.Models.Services.Contracts;
using MasterDetailSample01.ResponseFrameworks;
using MasterDetailSample01.ResponseFrameworks.Contracts;

namespace MasterDetailSample01.ApplicationServices.services
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IProductRepository  _productRepository ;

        #region [- ctor -]
        public ProductApplicationService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        } 
        #endregion

        public Task<IResponse<DeleteProductDto>> DeleteAsync(DeleteProductDto obj)
        {
            throw new NotImplementedException();
        }
 
        public Task<IResponse<PostProductDto>> PostAsync(PostProductDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<PutProductDto>> PutAsync(PutProductDto obj)
        {
            throw new NotImplementedException();
        }

        #region [- GetAsync() -]
        public async Task<IResponse<GetByIdProductDto>> GetAsync(GetByIdProductDto obj)
        {
            if (obj == null)
            {
                return new Response<GetByIdProductDto>
                    (false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.NullInput,
                    null
                    );
            }
            var product = new Product()
            {
                Id = obj.Id
            };
            var result = await _productRepository.selectAsync(product);
            if (result.Value == null)
            {
                return new Response<GetByIdProductDto>(
                    false,
                    HttpStatusCode.NotFound,
                    "Product not found",
                    null
                );
            }
            var dto = new GetByIdProductDto()
            {
                ProductName = result.Value.ProductName,
                UnitPrice = result.Value.UnitPrice
            };
            return new Response<GetByIdProductDto>
               (true,
               HttpStatusCode.OK,
               ResponseMessages.SuccessfullOperation,
               dto
               );
        } 
        #endregion

        #region [- GetAllAsync() -]
        public async Task<IResponse<IEnumerable<GetAllProductDto>>> GetAllAsync()
        {
            var result = await _productRepository.selectAllAsync();
            if (result is null)
            {
                return new Response<IEnumerable<GetAllProductDto>>
               (false,
               HttpStatusCode.NotFound,
               ResponseMessages.Error,
               null
               );
            }

            var productDto = result.Value.Select(p => new GetAllProductDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                UnitPrice = p.UnitPrice
            });
            return new Response<IEnumerable<GetAllProductDto>>
                (true,
                HttpStatusCode.OK,
                ResponseMessages.SuccessfullOperation,
                productDto
                );
        }

        Task<IResponse<PutProductDto>> IApplicationService<PostProductDto, PutProductDto, DeleteProductDto, GetByIdProductDto, GetAllProductDto>.PutAsync(PutProductDto obj)
        {
            throw new NotImplementedException();
        }

        Task<IResponse<DeleteProductDto>> IApplicationService<PostProductDto, PutProductDto, DeleteProductDto, GetByIdProductDto, GetAllProductDto>.DeleteAsync(DeleteProductDto obj)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
