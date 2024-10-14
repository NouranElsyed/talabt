using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using talabat.Core.Entities.Identity;
using talabt.Controllers;
using talabt.Error;
using talabtAPIs.DTOs;

namespace talabtAPIs.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
    {
            _userManager = userManager;
        }
   
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Resgister(RegisterDTO model)
            {
            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.DisplayName,
                PhoneNumber = model.PhoneNumber

            };
            var Result = await _userManager.CreateAsync(user);
            if (!Result.Succeeded) return BadRequest(new ApiErrorResponse(400));
            var ReturnedUser = new UserDTO() 
            {
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = "ThisWillBeToken"
            
            };
            }
    }
}
