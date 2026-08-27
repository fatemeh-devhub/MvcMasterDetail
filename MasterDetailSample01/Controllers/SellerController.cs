using MasterDetailSample01.ApplicationServices.services.Contracts;
using MasterDetailSample01.ApplicationServices.Dtos.SellerDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasterDetailSample01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SellerController : ControllerBase
   
    {
        private readonly ISellerApplicationService _sellerApplicationService;

        #region [- ctor -]
        public SellerController(ISellerApplicationService sellerApplicationService)
        {
            _sellerApplicationService = sellerApplicationService;
        }
        #endregion

        #region [- Post() -]
        [HttpPost]
        public async Task<IActionResult> PostSeller(PostSellerDto obj)
        {
            var result = await _sellerApplicationService.PostAsync(obj);
            if (!result.IsSuccessful)
                return BadRequest(result.Message);
            return Ok(result.Value);
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
