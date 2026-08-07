using System.Net;
using MasterDetailSample01.ResponseFrameworks;
using MasterDetailSample01.Models.Services.Contracts;
using MasterDetailSample01.ResponseFrameworks.Contracts;
using MasterDetailSample01.Models.DomainModels.OrderAggregates;
using MasterDetailSample01.ApplicationServices.services.Contracts;
using MasterDetailSample01.ApplicationServices.Dtos.OrderHeaderDtos;
using MasterDetailSample01.ApplicationServices.Dtos.OrderDetailDtos;





namespace MasterDetailSample01.ApplicationServices.services
{
    public class OrderHeaderApplicationService :IOrderHeaderApplicationService
    {

        private readonly IOrderHeaderRepository _orderHeaderRepository;
        private readonly IProductRepository _productRepository;

     
        #region [- ctro -]
        public OrderHeaderApplicationService(IOrderHeaderRepository orderHeaderRepository,
            IProductRepository productRepository)
        {
            _orderHeaderRepository = orderHeaderRepository;
            _productRepository = productRepository;

        }
        #endregion

        #region [- PostAsync() -]
        public async Task<IResponse<PostOrderHeaderDto>> PostAsync(PostOrderHeaderDto obj)
        {
            if (obj is null)
            {
                return new Response<PostOrderHeaderDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.NullInput,
                    null);
            }

            if (obj.OrderDetails == null || !obj.OrderDetails.Any())
            {
                return new Response<PostOrderHeaderDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    "Order details are required.",
                    null);
            }

            
            if (obj.OrderDetails.Any(x => x.ParentGuid != obj.GuidKey))
            {
                return new Response<PostOrderHeaderDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    "One or more OrderDetails do not belong to this order",
                    null);
            }

         
            var orderHeader = new OrderHeader
            {
                GuidKey = obj.GuidKey,
                CustomerId = obj.CustomerId,
                SellerId = obj.SellerId,
                OrderDetails = new List<OrderDetail>()
            };

            foreach (var item in obj.OrderDetails)
            {
                var response = await _productRepository.selectAsync(new Product
                {
                    Id = item.ProductId
                });

                if (!response.IsSuccessful || response.Value == null)
                {
                    return new Response<PostOrderHeaderDto>(
                        false,
                        HttpStatusCode.NotFound,
                        "محصول یافت نشد.",
                        null);
                }

                orderHeader.OrderDetails.Add(new OrderDetail
                {
                    ParentGuid = item.ParentGuid,
                    ProductId = item.ProductId,
                    UnitPrice = response.Value.UnitPrice,
                    Quantity = item.Quantity
                });
            }

            await _orderHeaderRepository.InsertAsync(orderHeader);

            return new Response<PostOrderHeaderDto>(
                true,
                HttpStatusCode.OK,
                ResponseMessages.SuccessfullOperation,
                obj);
        }

        #endregion

        #region [- PutAsync() -]
        public async Task<IResponse<PutOrderHeaderDto>> PutAsync(PutOrderHeaderDto obj)
        {
           if(obj == null)
            {
                return new Response<PutOrderHeaderDto>
                    (false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.NullInput,
                    null
                    );
                 }
                var orderHeader = new OrderHeader
                {
                    Id = obj.Id,
                    GuidKey = obj.GuidKey,
                    CustomerId = obj.CustomerId,
                    SellerId = obj.SellerId,
                    OrderDetails = obj.OrderDetails.Select(x => new OrderDetail
                    {
                        Id = x.Id,
                        ParentGuid = x.ParentGuid,
                        ProductId = x.ProductId,
                        UnitPrice = x.UnitPrice,
                        Quantity = x.Quantity
                    }).ToList()
                };
                var result = await _orderHeaderRepository.UpdateAsync(orderHeader);
            return new Response<PutOrderHeaderDto>
                   (true,
                   HttpStatusCode.OK,
                   ResponseMessages.SuccessfullOperation,
                   obj
                   );
        }
        
        
        #endregion

        #region [- DeleteAsync() -]
        public async Task<IResponse<DeleteOrderHeaderDto>> DeleteAsync(DeleteOrderHeaderDto obj)
        {
            if (obj == null)
                return new Response<DeleteOrderHeaderDto>
                    (
                     false,
                     HttpStatusCode.BadRequest,
                     ResponseMessages.NullInput,
                     null
                    );
            var orderHeader = new OrderHeader
            {
                Id = obj.Id,
            };
            var result = _orderHeaderRepository.DeleteAsync(orderHeader);
            return new Response<DeleteOrderHeaderDto>
               (
                true,
                HttpStatusCode.OK,
                ResponseMessages.SuccessfullOperation,
                obj
                ); 
      
        } 
        #endregion

        #region [- GetAllAsync() -]
        public async Task<IResponse<IEnumerable<GetAllOrderHeaderDto>>> GetAllAsync()
        {
            var result = await _orderHeaderRepository.selectAllAsync();
            if (!result.IsSuccessful || result.Value is null)
            {
                return new Response<IEnumerable<GetAllOrderHeaderDto>>
                    (false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.NullInput,
                    null
                     );
            }
            var getOrderHeaderDto = result.Value.Select(o => new GetAllOrderHeaderDto
            {
                Id = o.Id,
                GuidKey = o.GuidKey,
                CustomerId=o.CustomerId,
                CustomerFirstName=o.Customer.CustomerFirstName,
                CustomerLastName=o.Customer.CustomerLastName,
                SellerId = o.SellerId,
                SellerFirstName = o.Seller.SellerFirstName,
                SellerLastName = o.Seller.SellerLastName,
                OrderDetails = o.OrderDetails.Select(od => new GetAllOrderDetailDto 
               {
                   ProductId=od.ProductId,
                   ProductName = od.Product.ProductName,
                   UnitPrice = od.UnitPrice,
                   Quantity =od.Quantity

               }).ToList()
            }).ToList();

            return new Response<IEnumerable<GetAllOrderHeaderDto>>
                    (true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    getOrderHeaderDto
                     );

        }
        #endregion

        #region [- GetAsync() -]
        public async Task<IResponse<OrderHeaderDetailDto>> GetAsync(OrderHeaderDetailDto obj)
        {
            if (obj is null)
            {
                return new Response<OrderHeaderDetailDto>(
                    false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.NullInput,
                    null);
            }
            var orderHeader = new OrderHeader()
            {
                Id = obj.Id
            };
            var response = await _orderHeaderRepository.selectAsync(orderHeader);
            if (!response.IsSuccessful || response.Value == null)
                return new Response<OrderHeaderDetailDto>(
                    false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.NullInput,
                    null);
            var result = new OrderHeaderDetailDto()
            {
                Id = response.Value.Id,
                CustomerFirstName =response.Value.Customer.CustomerFirstName,
                CustomerLastName = response.Value.Customer.CustomerLastName,
                OrderDetails = response.Value.OrderDetails,
 
            };

            return new Response<OrderHeaderDetailDto>
                (true,
                HttpStatusCode.OK,
                ResponseMessages.SuccessfullOperation,
                result);
        }

        //public async Task<IResponse<OrderViewModel>> GetOrderAsync(Guid orderId)
        //{
        //    // گرفتن سفارش از Repository

        //    // تبدیل Entity به ViewModel

        //    // return ViewModel
        //}
        #endregion
    }
}
