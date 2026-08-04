using System.Linq;
using System.Net;
using MasterDetailSample01.ApplicationServices.Dtos.SellerDtos;
using MasterDetailSample01.ApplicationServices.services.Contracts;
using MasterDetailSample01.Models.Services.Contracts;
using MasterDetailSample01.ResponseFrameworks;
using MasterDetailSample01.ResponseFrameworks.Contracts;

namespace MasterDetailSample01.ApplicationServices.services
{
    public class SellerApplicationService : ISellerApplicationService
    {
        private readonly ISellerRepository _sellerRepository;

        #region [- ctor -]
        public SellerApplicationService(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository;
        } 
        #endregion

        public Task<IResponse<bool>> DeleteAsync(DeleteSellerDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<GetByIdSellerDto>> GetAsync(GetByIdSellerDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<PostSellerDto>> PostAsync(PostSellerDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<bool>> PutAsync(PutSellerDto obj)
        {
            throw new NotImplementedException();
        }

        #region [- GetAllAsync() -]
        public async Task<IResponse<IEnumerable<GetAllSellerDto>>> GetAllAsync()
        {
            var result = await _sellerRepository.selectAllAsync();
            if (result is null)
                return new Response<IEnumerable<GetAllSellerDto>>
                    (false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.Error,
                    null
                    );
            var sellerDtos = result.Value.Select(s => new GetAllSellerDto
            {
                Id = s.Id,
                SellerFirstName = s.SellerFirstName,
                SellerLastName = s.SellerLastName
            });
            return new Response<IEnumerable<GetAllSellerDto>>
                  (true,
                  HttpStatusCode.OK,
                  ResponseMessages.SuccessfullOperation,
                  sellerDtos
                  );
        }

        Task<IResponse<PutSellerDto>> IApplicationService<PostSellerDto, PutSellerDto, DeleteSellerDto, GetByIdSellerDto, GetAllSellerDto>.PutAsync(PutSellerDto obj)
        {
            throw new NotImplementedException();
        }

        Task<IResponse<DeleteSellerDto>> IApplicationService<PostSellerDto, PutSellerDto, DeleteSellerDto, GetByIdSellerDto, GetAllSellerDto>.DeleteAsync(DeleteSellerDto obj)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
