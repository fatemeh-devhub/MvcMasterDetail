using MasterDetailSample01.ResponseFrameworks.Contracts;
using MasterDetailSample01.Models.Services.Contracts;
using MasterDetailSample01.ResponseFrameworks;
using Microsoft.EntityFrameworkCore;
using System.Net;
using MasterDetailSample01.Models.DomainModels.CustomerAggregates;

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

        public Task<IResponse<Guid>> InsertAsync(Seller obj)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<bool>> UpdateAsync(string json)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<bool>> DeleteAsync(string json)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<Seller>> selectAsync(Seller obj)
        {
            throw new NotImplementedException();
        }

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

        Task<IResponse<Seller>> IRepository<Seller>.InsertAsync(Seller obj)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<Seller>> UpdateAsync(Seller obj)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<Seller>> DeleteAsync(Seller obj)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
