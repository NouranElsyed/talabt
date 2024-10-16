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
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
            var Result = await _userManager.CreateAsync(user,model.Password);
            if (!Result.Succeeded) return BadRequest(new ApiErrorResponse(400));
            var ReturnedUser = new UserDTO() 
            {
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = "ThisWillBeToken"
            
            };
            return Ok(ReturnedUser);
            }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null) return Unauthorized(new ApiErrorResponse(400));
             var Result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!Result.Succeeded) return Unauthorized(new ApiErrorResponse(401)); 
               
                var ReturnedUser = new UserDTO()
                    {
                    DisplayName=user.DisplayName,
                    Email=user.Email,
                    Token = "ThisWillBeToken"

                };
            return Ok(ReturnedUser);
        }
    }
}
