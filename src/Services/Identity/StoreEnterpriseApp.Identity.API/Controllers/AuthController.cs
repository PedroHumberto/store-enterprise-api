using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreEnterpriseApp.Identity.API.Models;
using StoreEnterpriseApp.Identity.API.Services;

namespace StoreEnterpriseApp.Identity.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _singInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenService _tokenService;


        public AuthController(SignInManager<IdentityUser> singInManager, UserManager<IdentityUser> userManager, TokenService tokenService)
        {
            _singInManager = singInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("singup")]
        public async Task<ActionResult> SingUp(UserSingUp model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true,
                
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                
                return Ok(await GenerateJwt(model.Email));
            }

            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLogin model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _singInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

            if (result.Succeeded)
            {

                return Ok(await GenerateJwt(model.Email));
            }

            return BadRequest();

        }

        [HttpPost("/reset-password-token")]
        public async Task<ActionResult> ResetPasswordTokenRequestAsync(UserResetPasswordToken resetToken)
        {
            var identityUser = await _singInManager.UserManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == resetToken.Email.ToUpper());

            if (identityUser != null)
            {
                string resetCodeToken = await _singInManager.UserManager.GeneratePasswordResetTokenAsync(identityUser);

                return Ok(resetCodeToken);
            }
            return BadRequest();

        }
        [HttpPost("/reset-password")]
        public async Task<ActionResult> ResetPasswordAsync(UserResetPassword resetUserPassword)
        {
            var identityUser = await _singInManager.UserManager.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == resetUserPassword.Email.ToUpper());

            if (identityUser == null)
            {
                return Unauthorized();
            }

            var result = await _userManager.ResetPasswordAsync(identityUser, resetUserPassword.Token, resetUserPassword.Password);


            return Ok();

        }


        private async Task<UserResponseLogin> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);


            var identityClaims = await _tokenService.GetClaimsUserAsync(claims, user, userRoles);

            var encodedToken = _tokenService.TokenCodifier(identityClaims);


            var response = _tokenService.TokenResponse(encodedToken, user, claims);


            return response;
        }
    }
}
