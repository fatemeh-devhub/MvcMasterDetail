using MasterDetailSample01.Models.DomainModels.CustomerAggregates;
using MasterDetailSample01.ResponseFrameworks.Contracts;
using MasterDetailSample01.Models.Services.Contracts;
using MasterDetailSample01.ResponseFrameworks;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
        public Task<IResponse<Customer>> DeleteAsync(Customer obj)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region [- InsertAsync() -]
        public async Task<IResponse<Customer>> InsertAsync(Customer obj)
        {
            try
            {
                if (obj == null)
                {
                    return new Response<Customer>
                        (false,
                        HttpStatusCode.BadRequest,
                        ResponseMessages.NullInput,
                        null
                        );
                }
                   
                    await _context.AddAsync(obj);
                    await _context.SaveChangesAsync();
                return new Response<Customer>
                      (true,
                      HttpStatusCode.Created,
                      ResponseMessages.SuccessfullOperation,
                      obj
                      );
            }
            catch (Exception ex) 
            {
                return new Response<Customer>
                         (false,
                         HttpStatusCode.InternalServerError,
                         ex.Message,
                         null
                         );
            }

        }
        #endregion

        #region [- UpdateAsync() -]
        public Task<IResponse<Customer>> UpdateAsync(Customer obj)
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
        #endregion

    }
}
