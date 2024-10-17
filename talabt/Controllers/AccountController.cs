using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using talabat.Core.Entities.Identity;
using talabat.Core.ServicesContext;
using talabt.Controllers;
using talabt.Error;
using talabtAPIs.DTOs;
using talabtAPIs.Extensions;

namespace talabtAPIs.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,ITokenService tokenService,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        #region  Register
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
                Token = await _tokenService.CreateTokenAsync(user, _userManager)

            };
            return Ok(ReturnedUser);
            }

        #endregion

        #region Login
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
        #endregion

        #region GetCurrentUser
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            var ReturnedUser = new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };
            return Ok(ReturnedUser);
        }
        #endregion
        #region GetCurrentUser
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDTO>> GetAddressOfCurrentUser()
        {
            var user = await _userManager.FindUserByAddressAsync(User);
            var MappedAddress = _mapper.Map<Address,AddressDTO>(user?.Address);
            return Ok(MappedAddress);
        }
        #endregion


    }
}
