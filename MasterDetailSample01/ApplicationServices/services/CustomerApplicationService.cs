using System.Net;
using MasterDetailSample01.ApplicationServices.Dtos.CustomerDtos;
using MasterDetailSample01.ApplicationServices.services.Contracts;
using MasterDetailSample01.Models.DomainModels.CustomerAggregates;
using MasterDetailSample01.Models.Services.Contracts;
using MasterDetailSample01.ResponseFrameworks;
using MasterDetailSample01.ResponseFrameworks.Contracts;
using Microsoft.AspNetCore.Identity;

namespace MasterDetailSample01.ApplicationServices.services
{
    public class CustomerApplicationService : ICustomerApplicationService
    {
        private readonly ICustomerRepository _customerRepository;

        #region [- ctor -]
        public CustomerApplicationService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        #endregion

        #region [- DeleteAsync() -]
        public Task<IResponse<bool>> DeleteAsync(DeleteCustomerDto obj)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region [- PostAsync() -]
        public Task<IResponse<PostCustomerDto>> PostAsync(PostCustomerDto obj)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region [- PutAsync() -]
        public Task<IResponse<bool>> PutAsync(PutCustomerDto obj)
        {
            throw new NotImplementedException();
        }


        #endregion


        #region [- GetAsync() -]
        public Task<IResponse<GetByIdCustomerDto>> GetAsync(GetByIdCustomerDto obj)
        {
            throw new NotImplementedException();
        }


        #endregion
      
        
        #region [- GetAllAsync() -]
        public async Task<IResponse<IEnumerable<GetAllCustomerDto>>> GetAllAsync()
        {
           var customers = await _customerRepository.selectAllAsync();
            if (customers.Value is null)
            {
                return new Response<IEnumerable<GetAllCustomerDto>>(
                    false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.Error,
                    null
                );
            }

            var customerDtos = customers.Value.Select(c => new GetAllCustomerDto
            {
                Id = c.Id,
                CustomerFirstName = c.CustomerFirstName,
                CustomerLastName= c.CustomerLastName,
                PhoneNumber = c.PhoneNumber
            });

            return new Response<IEnumerable<GetAllCustomerDto>>(
                true,
                HttpStatusCode.OK,
                ResponseMessages.SuccessfullOperation,
                customerDtos
            );
        }

        Task<IResponse<PutCustomerDto>> IApplicationService<PostCustomerDto, PutCustomerDto, DeleteCustomerDto, GetByIdCustomerDto, GetAllCustomerDto>.PutAsync(PutCustomerDto obj)
        {
            throw new NotImplementedException();
        }

        Task<IResponse<DeleteCustomerDto>> IApplicationService<PostCustomerDto, PutCustomerDto, DeleteCustomerDto, GetByIdCustomerDto, GetAllCustomerDto>.DeleteAsync(DeleteCustomerDto obj)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
