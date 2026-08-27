using MasterDetailSample01.ApplicationServices.Dtos.ProductDtos;
using MasterDetailSample01.ApplicationServices.services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasterDetailSample01.Controllers
{
   [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
   
    {
        private readonly IProductApplicationService _productApplicationService;

        #region [- ctor -]
        public ProductController(IProductApplicationService productApplicationService)
        {
            _productApplicationService = productApplicationService;
        }
        #endregion

        #region [- Post() -]
        [HttpPost]
        public async Task<IActionResult> PostProduct(PostProductDto obj)
        {
            var result = await _productApplicationService.PostAsync(obj);
            if (!result.IsSuccessful)
                return BadRequest(result.Message);
            return Ok(result.Value);
        } 
        #endregion

        #region [- GetAsync() -]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(GetProductDto dto)
        {
            var result = await _productApplicationService.GetAsync(dto);
            if (!result.IsSuccessful)
                return BadRequest(result.Message);
            return Ok(result.Value);
        }
        #endregion

        #region [- GetAllAsync() -]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productApplicationService.GetAllAsync();
            if (!result.IsSuccessful)
                return BadRequest(result.Message);
            return Ok(result.Value);
        } 
        #endregion
    }
}
