using Microsoft.AspNetCore.Mvc;

namespace talabt.Error
{
    [Route("error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi=true)]
    public class ErrorsController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound,$"{code}"));  
        }
    }
}
