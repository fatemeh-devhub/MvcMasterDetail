using MasterDetailSample01.ApplicationServices.Dtos.SellerDtos;
using MasterDetailSample01.ApplicationServices.services.Contracts;
using MasterDetailSample01.Models.DomainModels.CustomerAggregates;
using MasterDetailSample01.ResponseFrameworks.Contracts;
using MasterDetailSample01.Models.Services.Contracts;
using MasterDetailSample01.ResponseFrameworks;
using System.Net;

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

        #region [- PostAsync() -]
        public async Task<IResponse<PostSellerDto>> PostAsync(PostSellerDto obj)
        {
            if (obj == null)
            {
                return new Response<PostSellerDto>
                    (false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.NullInput,
                    null
                    );
            }
            var seller = new Seller
            {
                SellerFirstName = obj.SellerFirstName,
                SellerLastName = obj.SellerLastName,
            };

            var result = await _sellerRepository.InsertAsync(seller);

            return new Response<PostSellerDto>
                   (true,
                   HttpStatusCode.OK,
                   ResponseMessages.SuccessfullOperation,
                   obj
                   );
        }
        #endregion

        #region [- PutAsync() -]
        public Task<IResponse<PutSellerDto>> PutAsync(PutSellerDto obj)
        {
            throw new NotImplementedException();
        }
        #endregion
       
        #region [- DeleteAsync() -]
        public Task<IResponse<DeleteSellerDto>> DeleteAsync(DeleteSellerDto obj)
        {
            throw new NotImplementedException();
        }
        #endregion

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
        #endregion
       
        #region [- GetAsync() -]
        public Task<IResponse<GetSellerDto>> GetAsync(GetSellerDto obj)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
