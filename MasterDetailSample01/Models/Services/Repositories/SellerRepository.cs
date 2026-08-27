using MasterDetailSample01.Models.DomainModels.CustomerAggregates;
using MasterDetailSample01.ResponseFrameworks.Contracts;
using MasterDetailSample01.Models.Services.Contracts;
using MasterDetailSample01.ResponseFrameworks;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace MasterDetailSample01.Models.Services.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        private readonly AppDbContext _context;

        #region [- ctor -]
        public SellerRepository(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region [- InsertAsync() -]
        public async Task<IResponse<Seller>> InsertAsync(Seller obj)
        {
            try
            {
                if (obj == null)
                {
                    return new Response<Seller>
                        (false,
                        HttpStatusCode.BadRequest,
                        ResponseMessages.NullInput,
                        null
                        );
                }
                await _context.AddAsync(obj);
                await _context.SaveChangesAsync();
                return new Response<Seller>
                      (true,
                      HttpStatusCode.Created,
                      ResponseMessages.SuccessfullOperation,
                      obj
                      );

            }
            catch (Exception ex)
            {
                return new Response<Seller>
                        (false,
                        HttpStatusCode.InternalServerError,
                        ex.Message,
                        null
                        );
            }
        }
        #endregion

        #region [- UpdateAsync() -]
        public Task<IResponse<Seller>> UpdateAsync(Seller obj)
        {
            throw new NotImplementedException();
        } 
        #endregion

        #region [- DeleteAsync() -]
        public Task<IResponse<Seller>> DeleteAsync(Seller obj)
        {
            throw new NotImplementedException();
        } 
        #endregion

        #region [- selectAsync() -]
        public Task<IResponse<Seller>> selectAsync(Seller obj)
        {
            throw new NotImplementedException();
        } 
        #endregion

        #region [- selectAllAsync() -]
        public async Task<IResponse<IEnumerable<Seller>>> selectAllAsync()
        {
            try
            {
                var Sellers = await _context.Set<Seller>().AsNoTracking().ToListAsync();
                return new Response<IEnumerable<Seller>>
                 (true,
                 HttpStatusCode.OK,
                 ResponseMessages.SuccessfullOperation,
                 Sellers
                 );
            }
            catch (Exception ex)
            {

                return new Response<IEnumerable<Seller>>
                    (false,
                    HttpStatusCode.InternalServerError,
                    ex.ToString(),
                    null
                    );
            }
        }

        #endregion
    }
}
