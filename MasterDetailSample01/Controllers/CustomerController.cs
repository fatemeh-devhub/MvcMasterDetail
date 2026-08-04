using MasterDetailSample01.ApplicationServices.services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace MasterDetailSample01.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerApplicationService _customerApplicationService;

        #region [- ctor -]
        public CustomerController(ICustomerApplicationService customerApplicationService)
        {
            _customerApplicationService = customerApplicationService;
        }
        #endregion

        #region [- GetAll() -]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _customerApplicationService.GetAllAsync();
            if (!result.IsSuccessful)
                return BadRequest(result.Message);
            return Ok(result.Value);
        }
        #endregion
    }


}
