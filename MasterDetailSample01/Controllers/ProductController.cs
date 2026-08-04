using MasterDetailSample01.ApplicationServices.Dtos.ProductDtos;
using MasterDetailSample01.ApplicationServices.services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace MasterDetailSample01.Controllers
{
    public class ProductController:Controller
    {
        private readonly IProductApplicationService _productApplicationService;

        #region [- ctor -]
        public ProductController(IProductApplicationService productApplicationService)
        {
            _productApplicationService = productApplicationService;
        } 
        #endregion

        #region [- GetAsync() -]
        [HttpGet]
        public async Task<IActionResult> GetAsync(GetByIdProductDto dto)
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
