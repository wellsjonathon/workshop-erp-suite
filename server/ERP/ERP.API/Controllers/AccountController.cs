using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ERP.Models;
using Microsoft.AspNetCore.Identity;
using ERP.Repositories.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ERP.API.Controllers
{
    [Route("account")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn([FromBody] IdentityUser user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, false, false);

            if (result.Succeeded)
            {
                var loggedInUser = _userManager.Users.SingleOrDefault(u => u.Email == user.Email);
                // TODO: Look into SignInResult return type
                return Ok(new
                {
                    token = GenerateJwtToken(user.Email, loggedInUser)
                });
            }

            // TODO: Could probably use a more descriptive error in this case
            return BadRequest("Invalid login information");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            // TODO: Logging out with JWT seems messy. Is this the right auth method?
            //  Does the JWT need to be invalidated? JWT is used more for authorizaton
            //  rather than authentication; Identity is used for authentication
            throw new NotImplementedException();
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] IdentityUser newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if a user with the same username exists
            // TODO: Determine if username is the best metric here
            var user = await _userManager.FindByNameAsync(newUser.UserName);
            if (user != null)
            {
                return Conflict();
            }

            user = new User { UserName = newUser.UserName, Email = newUser.Email };
            var result = await _userManager.CreateAsync(user, newUser.PasswordHash);
            if (result.Succeeded)
            {
                // TODO: Redirect to login? Or do so client-side upon receiving 200?
                return Ok("Registration successful, please login");
            }
            else
            {
                return BadRequest();
            }
        }

        // ===== Helper Functions =====
        private string GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:JwtIssuer"],
                _configuration["Jwt:JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}