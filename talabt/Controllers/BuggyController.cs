using Microsoft.AspNetCore.Mvc;
using talabat.Repository.Data;
using talabt.Error;

namespace talabt.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _storeContext;

        public BuggyController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        [HttpGet("notfound")]
        public  async  Task<IActionResult> NotFound()
        {
            var brand = await _storeContext.Brands.FindAsync(100);
            if (brand is null) return  NotFound(new ApiErrorResponse(404));
            return Ok(brand);
        }
        [HttpGet("servererror")]
        public async Task<IActionResult> serverError()
        {
            var brand = await _storeContext.Brands.FindAsync(100);
            var brandToString = brand.ToString();
            return Ok(brand);

        }
        [HttpGet("badrequest")]
        public async Task<IActionResult> BadRequest()
        {
            return BadRequest(new ApiErrorResponse(400));
        }
        [HttpGet("badrequest/{id}")]
        public async Task<IActionResult> validationerror()
        {
            return Ok();
        }
        [HttpGet("Unauthorized")]
        public async Task<IActionResult> Unauthorized()
        {
            return Unauthorized(new ApiErrorResponse(401));
        }

    }
}
