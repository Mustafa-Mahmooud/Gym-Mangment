using Core.CreateToken;
using Core.Entites.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOS;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IToken _token;

        public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager  , IToken token)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _token = token;
        }



        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Unauthorized();

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = model.Email,
                token = await _token.CreateTokenAsync(user, _userManager)
            });
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto RegistrationForm)
        {
            var NewUser = new AppUser()
            {
                Email = RegistrationForm.Email,
                DisplayName = RegistrationForm.DisplayName,
                UserName = RegistrationForm.Email.Split("@")[0],
                PhoneNumber = RegistrationForm.PhoneNumber,
            }; 


            var result = await _userManager.CreateAsync(NewUser , RegistrationForm.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors); // Return errors to help debugging


            return Ok(new UserDto()
            {
                DisplayName = RegistrationForm.DisplayName,
                Email = RegistrationForm.Email,
                token = await _token.CreateTokenAsync(NewUser, _userManager)
            });

        }
 






    }
}
