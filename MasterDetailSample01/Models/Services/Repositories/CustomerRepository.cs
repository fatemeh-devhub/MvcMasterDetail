using System.Linq.Expressions;
using System.Net;
using MasterDetailSample01.Models.DomainModels.CustomerAggregates;

using MasterDetailSample01.Models.Services.Contracts;
using MasterDetailSample01.ResponseFrameworks;
using MasterDetailSample01.ResponseFrameworks.Contracts;
using Microsoft.EntityFrameworkCore;

namespace MasterDetailSample01.Models.Services.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        #region [- ctor -]
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region [- DeleteAsync() -]
        public Task<IResponse<bool>> DeleteAsync(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region [- InsertAsync() -]
        public Task<IResponse<Guid>> InsertAsync(Customer obj)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region [- UpdateAsync() -]
        public Task<IResponse<bool>> UpdateAsync(string json)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region [- selectAsync() -]
        public Task<IResponse<Customer>> selectAsync(Customer obj)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region [- selectAllAsync() -]
        public async Task<IResponse<IEnumerable<Customer>>> selectAllAsync()
        {
            try
            {
                var customers = await _context.Set<Customer>().AsNoTracking().ToListAsync();
                 return new Response<IEnumerable<Customer>>
                        (true,
                        HttpStatusCode.OK,
                        ResponseMessages.SuccessfullOperation,
                        customers
                        );

            }
           
            catch (Exception ex)
            {
                return new Response<IEnumerable<Customer>>
                (
                    false,
                    HttpStatusCode.InternalServerError,
                    ex.ToString(),
                    null
                );
            }
        }

        Task<IResponse<Customer>> IRepository<Customer>.InsertAsync(Customer obj)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<Customer>> UpdateAsync(Customer obj)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<Customer>> DeleteAsync(Customer obj)
        {
            throw new NotImplementedException();
        }
        #endregion



    }
}
