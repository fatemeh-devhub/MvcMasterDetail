using MasterDetailSample01.ApplicationServices.Dtos.OrderHeaderDtos;
using MasterDetailSample01.ApplicationServices.services;
using MasterDetailSample01.ApplicationServices.services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace MasterDetailSample01.Controllers
{
    public class OrderController: Controller
    {
        private readonly IOrderHeaderApplicationService _orderHeaderApplicationService;

        #region [- ctor -]
        public OrderController(IOrderHeaderApplicationService orderHeaderApplicationService)
        {
            _orderHeaderApplicationService = orderHeaderApplicationService;
        }
        #endregion

        #region [- Index() -]
        public IActionResult Index() //Views/Order/Index.cshtml
        {
            return View();
        }
        #endregion

        #region [- post() -]
      
        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] PostOrderHeaderDto dto)
        {
           if (!ModelState.IsValid)
              return BadRequest(ModelState);
           
            var result = await _orderHeaderApplicationService.PostAsync(dto);

            if (!result.IsSuccessful)

                return BadRequest(result.Message);

            return Ok(result.Value);

        }
        #endregion

        #region [- put() -]

        [HttpPut]
        public async Task<IActionResult> PutOrder([FromBody] PutOrderHeaderDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _orderHeaderApplicationService.PutAsync(dto);

            if (!result.IsSuccessful)

                return BadRequest(result.Message);

            return Ok(result.Value);

        }
        #endregion

        #region [- Delete() -]
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder([FromBody] DeleteOrderHeaderDto dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _orderHeaderApplicationService.DeleteAsync(dto);

            if (!result.IsSuccessful)

                return BadRequest(result.Message);

            return Ok(result.Value);
        } 
        #endregion

        #region [- GetAll() -]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orderHeaders = await _orderHeaderApplicationService.GetAllAsync();

            return Ok(orderHeaders.Value);
        }
    } 
    #endregion
}
