using MasterDetailSample01.ApplicationServices.services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace MasterDetailSample01.Controllers
{
    public class SellerController : Controller
    {
        private readonly ISellerApplicationService _sellerApplicationService;

        #region [- ctor -]
        public SellerController(ISellerApplicationService sellerApplicationService)
        {
            _sellerApplicationService = sellerApplicationService;
        }
        #endregion

        #region [- GetAll() -]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _sellerApplicationService.GetAllAsync();
            if (!result.IsSuccessful)
                return BadRequest(result.Message);
            return Ok(result.Value);
        } 
        #endregion
    }
}
