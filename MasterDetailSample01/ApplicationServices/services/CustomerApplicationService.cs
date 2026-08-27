using MasterDetailSample01.ApplicationServices.Dtos.CustomerDtos;
using MasterDetailSample01.ApplicationServices.services.Contracts;
using MasterDetailSample01.Models.DomainModels.CustomerAggregates;
using MasterDetailSample01.ResponseFrameworks.Contracts;
using MasterDetailSample01.Models.Services.Contracts;
using MasterDetailSample01.ResponseFrameworks;
using System.Net;


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
        public Task<IResponse<DeleteCustomerDto>> DeleteAsync(DeleteCustomerDto obj)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region [- PostAsync() -]
        public async Task<IResponse<PostCustomerDto>> PostAsync(PostCustomerDto obj)
        {
            if (obj == null) 
            {
                return new Response<PostCustomerDto>
                    (false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.NullInput,
                    null
                    );
            }
            var customer = new Customer
            {
                CustomerFirstName = obj.CustomerFirstName,
                CustomerLastName = obj.CustomerLastName,
                PhoneNumber = obj.PhoneNumber,
            };
            var result = await _customerRepository.InsertAsync(customer);
            return new Response<PostCustomerDto>
                    (true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    obj
                    );
        }

        

        #endregion

        #region [- PutAsync() -]
        public Task<IResponse<PutCustomerDto>> PutAsync(PutCustomerDto obj)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region [- GetAsync() -]
        public Task<IResponse<GetCustomerDto>> GetAsync(GetCustomerDto obj)
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
      #endregion
    }
}
