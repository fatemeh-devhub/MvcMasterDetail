using System.Data;
using System.Net;
using System.Text.Json;
using MasterDetailSample01.Models.DomainModels.OrderAggregates;
using MasterDetailSample01.Models.Services.Contracts;
using MasterDetailSample01.ResponseFrameworks;
using MasterDetailSample01.ResponseFrameworks.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace MasterDetailSample01.Models.Services.Repositories
{
    public class OrderHeaderRepository : IOrderHeaderRepository
    {
        private readonly AppDbContext _context;


        #region [- ctor -]
        public OrderHeaderRepository(AppDbContext context)
        {
            _context = context;
        }


        #endregion

        #region [- InsertAsync() -]
        public async Task<IResponse<OrderHeader>> InsertAsync(OrderHeader obj)
        {
           try
            {
                var json = JsonSerializer.Serialize(obj);
                var param = new SqlParameter("@OrderJson", json);

                var orderId = _context.Database
                .SqlQueryRaw<Guid>(
                 "EXEC Usp_InsertOrder @OrderJson",
                 param)
                .AsEnumerable()
                .FirstOrDefault();

                return new Response<OrderHeader>
                (
                    true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    obj
                );
            }
            catch (Exception)
            {
                return new Response<OrderHeader>
              (
                  false,
                  HttpStatusCode.InternalServerError,
                  ResponseMessages.Error,
                 null
              );
            }
        }

        #endregion

        #region [- UpdateAsync() -]
        public async Task<IResponse<OrderHeader>> UpdateAsync(OrderHeader obj)
        {
            try
            {

                var json = JsonSerializer.Serialize(obj);

                var param = new SqlParameter("@OrderJson", json);

                await _context.Database.ExecuteSqlRawAsync(
                "EXEC Usp_UpdateOrder @OrderJson", param);


                return new Response<OrderHeader>
                (
                    true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    obj
                );
            }
            catch (Exception) 
            {
                return new Response<OrderHeader>
               (
                   true,
                   HttpStatusCode.InternalServerError,
                   ResponseMessages.Error,
                   null
               );

            }
        }
        #endregion

        #region [- DeleteAsync() -]

        public async Task<IResponse<OrderHeader>> DeleteAsync(OrderHeader obj)
        {
            var json = JsonSerializer.Serialize(obj);
            var param = new SqlParameter("@OrderId", json);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_DeleteOrder @OrderId",
                param);

            return new Response<OrderHeader>
            (
                true,
                HttpStatusCode.OK,
                ResponseMessages.SuccessfullOperation,
                obj
            );
        }

        #endregion

        #region [- selectAsync() -]
        //public async Task<IResponse<OrderHeader>> selectAsync(OrderHeader obj)
        //{
        //    try
        //    {
        //        if (obj == null)
        //        {
        //            return new Response<OrderHeader>
        //           (false,
        //            HttpStatusCode.BadRequest,
        //            ResponseMessages.Error,
        //            null);
        //        }

        //        var orderHeader = await _context.OrderHeaders
        //       .Include(x => x.Customer)
        //       .Include(x => x.Seller)
        //       .Include(x => x.OrderDetails)
        //       .AsNoTracking()
        //       .ToListAsync();

        //        return new Response<OrderHeader>
        //           (true,
        //            HttpStatusCode.OK,
        //            ResponseMessages.SuccessfullOperation,
        //            orderHeader);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response<OrderHeader>
        //        (false,
        //         HttpStatusCode.InternalServerError,
        //         ex.ToString(),
        //         null);

        //    }
        //}


        public Task<IResponse<OrderHeader>> selectAsync(OrderHeader obj)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region [- selectAllAsync() -]
        public async Task<IResponse<IEnumerable<OrderHeader>>> selectAllAsync()
        {
            try
            {
                var orders = await _context.Set<OrderHeader>()
                     .AsNoTracking()
                    .Include(x => x.Customer)
                    .Include(x => x.Seller)
                    .Include(x => x.OrderDetails)
                    .ThenInclude(d => d.Product)
                    .ToListAsync();


                return new Response<IEnumerable<OrderHeader>>
                (true,
                HttpStatusCode.OK,
                ResponseMessages.SuccessfullOperation,
                orders
                );
            }
            catch (Exception)
            {
                return new Response<IEnumerable<OrderHeader>>
                 (false,
                 HttpStatusCode.InternalServerError,
                 ResponseMessages.Error,
                 null
                    );

            }

        }

        #endregion
    }

}


    

    


    