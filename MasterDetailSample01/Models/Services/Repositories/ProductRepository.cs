using System.Net;
using MasterDetailSample01.Models.DomainModels.CustomerAggregates;
using MasterDetailSample01.Models.Services.Contracts;
using MasterDetailSample01.ResponseFrameworks;
using MasterDetailSample01.ResponseFrameworks.Contracts;
using Microsoft.EntityFrameworkCore;

namespace MasterDetailSample01.Models.Services.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        #region [- ctor -]
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        public Task<IResponse<Product>> InsertAsync(Product obj)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<Product>> UpdateAsync(Product obj)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<Product>> DeleteAsync(Product obj)
        {
            throw new NotImplementedException();
        }
        #region [- SelectAsync() -]
        public async Task<IResponse<Product>> selectAsync(Product obj)
        {

            try
            {
                //var product = await _context.Set<Product>().FindAsync(obj.Id);
                var product = _context.Set<Product>().SingleOrDefault(p => p.Id == obj.Id);

                return new Response<Product>
                    (true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    product
                    );


            }
            catch (Exception ex)
            {
                return new Response<Product>
                       (false,
                       HttpStatusCode.InternalServerError,
                       ex.ToString(),
                       null
                       );
            }
        }
        #endregion


        #region [- selectAllAsync() -]
        public async Task<IResponse<IEnumerable<Product>>> selectAllAsync()
        {

            try
            {
                var products = await _context.Set<Product>().AsNoTracking().ToListAsync();
                return new Response<IEnumerable<Product>>
                 (true,
                 HttpStatusCode.OK,
                 ResponseMessages.SuccessfullOperation,
                 products
                 );
            }
            catch (Exception ex)
            {

                return new Response<IEnumerable<Product>>
                    (false,
                    HttpStatusCode.InternalServerError,
                    ex.ToString(),
                    null
                    );
            }
            #endregion
        }
    }
}
