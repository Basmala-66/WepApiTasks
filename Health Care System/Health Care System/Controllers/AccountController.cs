using Health_Care_System.DTOs.AccountDtos;
using Health_Care_System.Entities;
using Health_Care_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Health_Care_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
            };

            var result = await _userManager.CreateAsync(user,request.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);


            return Ok("User registered successfully");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequset requset)
        {
            var user  =await _userManager.FindByEmailAsync(requset.Email);


            if (user == null)
                return Unauthorized("invalid email or password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, requset.Password, false);

            if (!result.Succeeded)
                return Unauthorized("invalid email or password");

            var token = _jwtService.GenerateToken(user);

            return Ok(new {Token= token});
        }
    }
}
